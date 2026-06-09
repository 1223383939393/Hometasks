# Архитектура системы SmartDelivery

Этот раздел описывает внутреннее устройство сервиса и взаимодействие компонентов.

---

## Диаграмма компонентов системы

<div class="mermaid">
graph TB
  subgraph Клиент["Клиентский уровень"]
    MA[Мобильное приложение]
    WA[Веб-приложение]
    EXT[Внешние системы]
  end

  subgraph API["API Gateway / Сервер"]
    GW[API Gateway]
    AUTH[Сервис аутентификации]
    ORDERS[Сервис заказов]
    NOTIFY[Сервис уведомлений]
  end

  subgraph DATA["Уровень данных"]
    DB[(PostgreSQL БД)]
    CACHE[(Redis Cache)]
    QUEUE[RabbitMQ Queue]
  end

  subgraph EXT_SVC["Внешние сервисы"]
    PAY[Платёжный шлюз]
    COURIER[Сервис курьеров]
    SMS[SMS/Email провайдер]
  end

  MA --> GW
  WA --> GW
  EXT --> GW

  GW --> AUTH
  GW --> ORDERS

  ORDERS --> DB
  ORDERS --> CACHE
  ORDERS --> QUEUE

  QUEUE --> NOTIFY

  ORDERS --> PAY
  ORDERS --> COURIER

  NOTIFY --> SMS
</div>

---

## Диаграмма последовательности: жизненный цикл заказа

<div class="mermaid">
sequenceDiagram
  participant C as Клиент
  participant A as SmartDelivery API
  participant D as База данных
  participant P as Платёжный шлюз
  participant K as Сервис курьеров

  C->>A: POST /api/orders (создание заказа)
  A->>D: Проверка наличия товаров
  D-->>A: Товары в наличии
  A->>D: CREATE Order (status=PENDING)
  D-->>A: Order ID сохранён
  A-->>C: 201 Created + order_id

  Note over C,A: Клиент переходит к оплате

  C->>A: POST /api/orders/{id}/pay
  A->>D: UPDATE Order (status=RESERVED)
  A->>P: Process payment
  P-->>A: Payment Success
  A->>D: UPDATE Order (status=PAID)
  D-->>A: OK

  Note over A,K: Назначение курьера

  A->>K: Assign delivery для Order {id}
  K-->>A: Courier assigned
  A->>D: UPDATE Order (status=DELIVERING)
  A-->>C: 200 OK — курьер в пути

  Note over C,K: Курьер доставляет заказ

  K->>A: Delivery confirmed
  A->>D: UPDATE Order (status=DELIVERED)
  A-->>C: Заказ доставлен
</div>

---

## Flowchart: процесс обработки заказа

<div class="mermaid">
flowchart TD
  START([Клиент делает заказ]) --> VALIDATE{Валидация данных}

  VALIDATE -- Ошибка --> ERR400([400 Bad Request])
  VALIDATE -- OK --> CHECK_STOCK{Товар в наличии?}

  CHECK_STOCK -- Нет --> ERR409([409 Out of Stock])
  CHECK_STOCK -- Да --> CREATE_ORDER[Создать заказ<br/>status=PENDING]

  CREATE_ORDER --> RESERVE[Зарезервировать товары на складе]
  RESERVE --> PAYMENT{Оплата прошла?}

  PAYMENT -- Ошибка --> CANCEL_RESERVE[Снять бронь]
  CANCEL_RESERVE --> ERR402([402 Payment Failed])

  PAYMENT -- Успех --> UPDATE_PAID[status=PAID]
  UPDATE_PAID --> ASSIGN_COURIER[Назначить курьера]

  ASSIGN_COURIER --> COURIER_FOUND{Курьер доступен?}
  COURIER_FOUND -- Нет --> QUEUE_WAIT[В очередь ожидания]
  QUEUE_WAIT --> ASSIGN_COURIER

  COURIER_FOUND -- Да --> DELIVERING[status=DELIVERING]

  DELIVERING --> DELIVERED{Доставлено?}
  DELIVERED -- Да --> DONE([status=DELIVERED])
  DELIVERED -- Проблема --> INCIDENT[Создать инцидент]
  INCIDENT --> SUPPORT[Служба поддержки]
</div>

---

## Диаграмма состояний заказа

<div class="mermaid">
stateDiagram-v2
  [*] --> PENDING : POST /api/orders
  PENDING --> RESERVED : Товары зарезервированы
  PENDING --> CANCELLED : Отмена клиентом

  RESERVED --> PAID : Оплата успешна
  RESERVED --> CANCELLED : Ошибка оплаты / таймаут

  PAID --> DELIVERING : Курьер назначен
  PAID --> CANCELLED : Отмена до передачи курьеру

  DELIVERING --> DELIVERED : Вручено получателю
  DELIVERING --> CANCELLED : Проблема с доставкой

  CANCELLED --> [*]
  DELIVERED --> [*]
</div>

---

## Технологический стек

| Компонент          | Технология         |
|--------------------|-------------------|
| API сервер         | Python / FastAPI  |
| База данных        | PostgreSQL 15     |
| Кэш                | Redis 7           |
| Очередь сообщений  | RabbitMQ          |
| Контейнеризация    | Docker / Kubernetes |
| CI/CD              | GitHub Actions    |