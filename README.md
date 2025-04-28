
# Sales API - Developer Evaluation

## 🛠 Technologies
- .NET 8
- AutoMapper
- FluentValidation
- DDD + CQRS Architecture

---

## 🚀 How to Run the Project

```bash
# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update

# Run the application
dotnet run
```

Swagger will be available at: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

---

## 📚 Main Endpoints (Sales)

| Method | Endpoint          | Action                       |
|:-------|:------------------|:-----------------------------|
| POST   | /api/sales         | Create a new sale            |
| GET    | /api/sales/{id}    | Retrieve a sale by ID        |
| PUT    | /api/sales/{id}    | Update an existing sale      |
| DELETE | /api/sales/{id}    | Cancel a sale (soft delete)  |

---

## 📝 Business Rules

- 4 or more identical items: **10% discount**
- Between 10 and 20 identical items: **20% discount**
- It is **not allowed** to sell more than 20 identical items
- **No discount** for purchases of fewer than 4 identical items

---

## 📤 Example Payload - Create Sale (`POST /api/sales`)

```json
{
  "saleNumber": "S-1001",
  "saleDate": "2025-04-26T00:00:00",
  "customer": "John Doe",
  "branch": "São Paulo Branch",
  "items": [
    {
      "productName": "Ambev Beer 350ml",
      "quantity": 6,
      "unitPrice": 5.00
    },
    {
      "productName": "Guaraná Antarctica 2L",
      "quantity": 2,
      "unitPrice": 8.50
    }
  ]
}
```

---

✅ Everything is ready to build, run, and test the API!

---

# 🎯 Notes:
- All database entities are mapped.
- The `SalesController` includes CRUD operations.
- Events (`SaleCreated`, `SaleModified`, `SaleCancelled`, `ItemCancelled`) are logged automatically.
- Proper dependency injection (IoC) is configured.
- Validation errors return descriptive messages.

---