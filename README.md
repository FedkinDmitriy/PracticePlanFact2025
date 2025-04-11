# PracticePlanFact2025 Web API

REST API-проект на ASP.NET Core для управления клиентами и заказами.

## 📦 Функциональность

- CRUD-операции для клиентов и заказов
- Фильтрация и пагинация
- Документация API через Swagger (OpenAPI)
- Внедрение зависимостей (Dependency Injection)
- PostgreSQL в качестве базы данных

## 🛠️ Технологии

- NET 9 (Preview)
- ASP.NET Core 9
- Entity Framework Core 9
- PostgreSQL (через Npgsql)
- Swagger / OpenAPI (Swashbuckle.AspNetCore)
- Dependency Injection (встроенный DI-контейнер ASP.NET Core)

## 🚀 Запуск проекта

1. Настрой файл `appsettings.json`, добавив строку подключения к PostgreSQL:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=mydb;Username=myuser;Password=mypassword"
    }
    ```

2. Установи зависимости (если не установлены):
    ```bash
    dotnet restore
    ```

3. Примените миграции:
    ```bash
    dotnet ef database update
    ```

4. Запусти приложение:
    ```bash
    dotnet run --project WebAPI
    ```

5. Открой Swagger:
    ```
    https://localhost:порт
    ```

## ✅ Примеры запросов

- `GET /api/clients`
- `POST /api/orders`
- `GET /api/clients/{id}`
- и другие — доступны в Swagger-интерфейсе

## 🧪 Тесты

(в разработке)

---

Проект создан в рамках учебного задания. Автор: **Федькин Дмитрий**
