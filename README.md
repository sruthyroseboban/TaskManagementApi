# Task Management API

This project is a simple Task Management API created as part of a technical assessment for a Back-End Developer role.

The application is built using ASP.NET Core (.NET 8) and focuses on clean structure, dependency injection, and basic backend concepts.

---

## What the project does

- Allows creating, viewing, updating, and deleting tasks
- Uses a repository pattern to separate data access from business logic
- Uses dependency injection with different service lifetimes
- Runs a background service every 30 seconds to check for upcoming tasks
- Connects to a public external API and handles possible errors
- Provides Swagger UI for testing the APIs

---

## Technologies used

- ASP.NET Core Web API (.NET 8)
- C#
- Swagger (OpenAPI)
- In-memory data storage
- HttpClientFactory

---

## API Endpoints

### Task APIs
- `GET /api/task` – Get all tasks  
- `GET /api/task/{id}` – Get a task by ID  
- `POST /api/task` – Create a new task  
- `PUT /api/task/{id}/complete` – Mark a task as completed  
- `DELETE /api/task/{id}` – Delete a task  

### External API
- `GET /api/external/sample` – Fetches sample data from a public API

---

## Dependency Injection

The project uses dependency injection throughout the application.

Different lifetimes are used:
- Singleton: Used for shared services like system time
- Scoped: Used for task repository and task service
- Transient: Used for request-based services

---

## Background Service

A background service runs every 30 seconds.
It checks for tasks that are due soon and logs a message.
This service uses scoped services safely through a service scope.

---

## External API Integration

A public API is accessed using HttpClientFactory.
A timeout is configured.
Errors such as timeouts and failed responses are handled gracefully.

---

## How to run the project

1. Clone the repository
2. Open the project in Visual Studio or VS Code
3. Run the following command:

```bash
dotnet run

Author
Sruthi Rose Boban