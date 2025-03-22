# Task Manager


- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Docker (Recommended)](#docker-recommended)
  - [Local Setup (Without Docker)](#local-setup-without-docker)
- [Running Unit Tests](#running-unit-tests)

## Features

- Create, read, update, and delete tasks (CRUD operations).
- User-friendly interface.
- Database persistence (SQL Server).
- Mediator.
- Docker & docker compose.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- **Docker (Recommended):** If using Docker, you'll need Docker and Docker Compose installed.
  - [Docker Installation Guide](https://docs.docker.com/get-docker/)
- **.NET SDK (Without Docker):** If running locally, you'll need the .NET SDK.
  - [.NET SDK Installation Guide](https://dotnet.microsoft.com/download)
- **SQL Server (Without Docker):** A running instance of SQL Server.
  - [SQL Server Installation Guide](https://docs.microsoft.com/en-us/sql/sql-server/install/installation-for-sql-server?view=sql-server-ver16)

### Docker (Recommended)

This application is containerized using Docker Compose for easy setup.

1.  **Clone the Repository:**

    ```bash
    git clone https://github.com/Igor-Vicente/TaskManager.git
    cd TaskManager
    ```

2.  **Navigate to the Docker Directory:**

    ```bash
    cd docker
    ```

3.  **Start the Application:**

    ```bash
    docker compose up -d
    ```

4.  **Access the Application:**
    Open your web browser and navigate to `http://localhost:8888`.

### Local Setup (Without Docker)

1.  **Clone the Repository:**

    ```bash
    git clone https://github.com/Igor-Vicente/TaskManager.git
    cd TaskManager
    ```

2.  **Configure Database Connection:**

    - Navigate to `/src/TaskManager.Presentation/appSettings.Development.json`.
    - Update the `ConnectionStrings` section with your SQL Server connection details.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
      }
    }
    ```

3.  **Run the Backend API:**

    - Navigate to `/src/TaskManager.Presentation/`.
    - Execute the following command:
      ```bash
      dotnet watch run
      ```

4.  **Run the Frontend:**
    - See the [Client README](src/TaskManager.Client/README.md) for instructions on running the frontend application.
    - Navigate to `/src/TaskManager.Client/` and follow the instructions in its README.md file.

## Running Unit Tests

To run the unit tests, execute the following command from the root directory of the project:

```bash
dotnet test
```
