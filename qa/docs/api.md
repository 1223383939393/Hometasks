# API SmartDelivery

## Общие сведения

Базовый путь API: `/api`

Формат запросов и ответов: JSON.

## POST /api/orders

Создание нового заказа.

### Запрос

```json
{
  "customerName": "Марат Сафаров",
  "address": "г. Кострома, ул. Примерная, 10",
  "items": [
    {
      "productId": 101,
      "name": "Пицца Маргарита",
      "quantity": 2,
      "price": 450
    }
  ],
  "paymentMethod": "card"
}
```

### Ответ

```json
{
  "id": 1001,
  "status": "PENDING",
  "customerName": "Марат Сафаров",
  "address": "г. Кострома, ул. Примерная, 10",
  "total": 900,
  "createdAt": "2026-06-09T09:00:00Z"
}
```

## GET /api/orders/{id}

Получение информации о заказе по идентификатору.

### Ответ

```json
{
  "id": 1001,
  "status": "PAID",
  "customerName": "Марат Сафаров",
  "address": "г. Кострома, ул. Примерная, 10",
  "items": [
    {
      "productId": 101,
      "name": "Пицца Маргарита",
      "quantity": 2,
      "price": 450
    }
  ],
  "total": 900,
  "createdAt": "2026-06-09T09:00:00Z"
}
```

## PUT /api/orders/{id}/status

Обновление статуса заказа.

### Запрос

```json
{
  "status": "DELIVERING"
}
```

### Ответ

```json
{
  "id": 1001,
  "status": "DELIVERING",
  "message": "Статус заказа обновлен"
}
```

## GET /api/orders

Получение списка заказов.

### Ответ

```json
[
  {
    "id": 1001,
    "status": "DELIVERING",
    "customerName": "Марат Сафаров",
    "total": 900
  },
  {
    "id": 1002,
    "status": "PENDING",
    "customerName": "Иван Петров",
    "total": 1200
  }
]
```

## Коды ответа

- 200 — успешный запрос.
- 201 — объект создан.
- 400 — ошибка валидации.
- 404 — заказ не найден.
- 500 — внутренняя ошибка сервера.