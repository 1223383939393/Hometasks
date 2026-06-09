# Тестирование API

В проекте используется публичный сервис JSONPlaceholder, который эмулирует работу REST API и позволяет тестировать CRUD-операции.

## Установка зависимостей

Перед запуском тестов установите зависимости:

```bash
python -m pip install -r requirements.txt
```

## Запуск тестов (Python-скрипт)

Запуск напрямую из Python:

```bash
python tests/api_tests.py
```

Скрипт выполняет последовательность запросов к `https://jsonplaceholder.typicode.com`:

- POST /posts — создание ресурса (аналог создания заказа).
- GET /posts/{id} — чтение.
- PUT /posts/{id} — обновление.
- DELETE /posts/{id} — удаление.

Результаты (статусы и ответы) выводятся в консоль.

## Запуск тестов через bat-скрипт

Для автоматического запуска и сохранения результата используйте батник:

```bash
tests\run_tests.bat
```

Скрипт:

- запускает `tests\api_tests.py`;
- записывает вывод в файл `tests\results.log`;
- отображает содержимое лога в консоли.

## Пример вывода

```text
=== Тестирование API SmartDelivery ===

POST /posts
Status: 201
...

GET /posts/{id}
Status: 200
...

PUT /posts/{id}
Status: 200
...

DELETE /posts/{id}
Status: 200
...
```