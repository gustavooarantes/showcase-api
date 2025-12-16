# FoodNet API

A robust, high-performance RESTful API built with **.NET 10** to simulate a restaurant's modern backend architecture.

## 📂 Project Structure

The solution follows a clean N-Layer architecture, promoting separation of concerns and scalability.

```text
FoodNet
├── BackgroundServices   # Background jobs (Hosted Services) consuming RabbitMQ
├── Controllers          # API Entry points
├── Data                 # DbContext and EF Core configurations
├── DTOs                 # Data Transfer Objects (Records)
├── Entities             # Domain Models (Database Tables)
│   └── Enums            # Enumerations (e.g., OrderStatus)
├── Repositories         # Data Access Layer (Repository Pattern)
├── Services             # Business Logic Layer
└── Program.cs           # Dependency Injection & App Configuration
```

---

## Features

- Product Management: Full CRUD operations for menu items.

- High-Performance Caching: Implemented Redis (Cache-Aside Pattern) for product queries to drastically reduce database load.

- Order Management System: Complex order creation with automatic price calculation and validation.

- Asynchronous Processing: Decoupled architecture using RabbitMQ. Order events are published to a queue for asynchronous handling.

- Background Workers: Native .NET `BackgroundService` acts as a consumer to process messages off the main thread.

- Persistence: Relational data storage using PostgreSQL with Entity Framework Core.

- Documentation: Interactive API documentation using Scalar (Next-gen OpenAPI UI).

- DevOps Ready: Fully containerized environment with Docker and Docker Compose.

---

## Tech Stack

- Framework: .NET 10 (Preview) / C#

- ORM: Entity Framework Core

- Database: PostgreSQL 15

- Caching: Redis (StackExchange)

- Messaging: RabbitMQ

- Documentation: OpenAPI / Scalar

- Containerization: Docker, docker-compose

---

## API Endpoints Overview

#### Products (/api/products)

- `GET /` - List all products (⚡ Cached via Redis)

- `GET /{id}` - Get details of a specific product.

- `POST /` - Create a new product (Invalidates Cache & Triggers RabbitMQ event).

#### Orders (/api/orders)

- `GET /` - List all orders with their items.

- `POST /` - Create a new complex order.

- Logic: Validates product existence, fetches real-time prices from DB, calculates total, and sets status to Pending.
