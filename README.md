# WebApiDemo

## Overview

WebApiDemo is a robust ASP.NET Core 8.0 Web API designed to provide secure authentication (JWT-based) and comprehensive CRUD operations for managing shirts. The project includes a dedicated client application (**WebApp**) that interacts seamlessly with the API.

## Features

- **ASP.NET Core 8.0 Web API** for efficient back-end development
- **JWT-based authentication** to secure endpoints
- **Full CRUD operations** for managing shirts
- **Entity Framework Core** with database migrations
- **Custom filters** for validation, authentication, and exception handling
- **WebApp client** to interact with the API easily

## Technologies Used

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **C#**

## Installation

### Prerequisites

Before running the project, ensure you have the following installed:

- **.NET 8.0 SDK**
- **SQL Server**
- **Visual Studio 2022** (recommended)

### Setup Steps

1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-username/WebApiDemo.git
   cd WebApiDemo
   ```
2. **Update the database connection string** in `appsettings.json`.
3. **Apply database migrations:**
   ```sh
   dotnet ef database update
   ```
4. **Run the API service:**
   ```sh
   dotnet run --project WebApiDemo
   ```
5. **Run the client application:**
   ```sh
   dotnet run --project WebApp
   ```

## API Endpoints

| Method | Endpoint          | Description                   |
| ------ | ----------------- | ----------------------------- |
| **POST**   | `/api/auth/login` | Authenticate user and obtain JWT |
| **GET**    | `/api/shirt`      | Retrieve all shirts             |
| **GET**    | `/api/shirt/{id}` | Retrieve a specific shirt by ID |
| **POST**   | `/api/shirt`      | Create a new shirt              |
| **PUT**    | `/api/shirt/{id}` | Update an existing shirt        |
| **DELETE** | `/api/shirt/{id}` | Remove a shirt                  |

## Contributing

We welcome contributions! If you’d like to make improvements, please submit a pull request. For major changes, open an issue first to discuss your proposal.


