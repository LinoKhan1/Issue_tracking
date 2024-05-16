# Issue Tracking Application

## Table of Contents
- [Introduction](#introduction)
- [Project Directory Structure](#project-directory-structure)
- [Tech Stack](#tech-stack)
- [Functional Requirements](#functional-requirements)
- [Technical Requirements](#technical-requirements)
- [Setup and Installation](#setup-and-installation)
- [Running the Application](#running-the-application)
- [Running the Tests](#running-the-tests)
- [Integration Tests for Delete Methods](#integration-tests-for-delete-methods)
- [Contributing](#contributing)
- [License](#license)

## Introduction
The Issue Tracking Application is a web-based tool designed to help teams manage and track issues or tasks in a project. Users can create, edit, view, and delete issues. The application provides search functionality to filter issues based on various criteria.

## Project Directory Structure

IssueTracking/
├── IssueTracking.sln
├── Tracking/
│   ├── Controllers/
│   │   └── IssueController.cs
│   ├── Data/
│   │   └── IssueContext.cs
│   ├── Models/
│   │   └── Issue.cs
│   ├── Repositories/
│   │   ├── IIssueRepository.cs
│   │   ├── IssueRepository.cs
│   │   └── RepositoryBase.cs
│   ├── unitOfWork/
│   │   ├── IUnitOfWork.cs
│   │   └── UnitOfWork.cs
│   ├── Views/
│   │   └── Issue/
│   │       ├── Create.cshtml
│   │       ├── Delete.cshtml
│   │       ├── Details.cshtml
│   │       ├── Edit.cshtml
│   │       └── Index.cshtml
│   ├── Tracking.csproj
│   └── wwwroot/
├── Tracking.Tests/
│   ├── Controllers/
│   │   └── IssueControllerTests.cs
│   ├── Repositories/
│   │   └── IssueRepositoryTests.cs
│   ├── unitOfWork/
│   │   └── UnitOfWorkTests.cs
│   ├── Tracking.Tests.csproj
│   └── IntegrationTests/
│       ├── IssueRepositoryIntegrationTests.cs
│       └── IssueControllerIntegrationTests.cs
├── README.md
└── IssueTracking.sln


## Tech Stack
- **Backend**: ASP.NET Core
- **Frontend**: Razor Pages
- **Database**: Entity Framework Core (with SQLite for integration tests)
- **Testing**: xUnit, Moq

## Functional Requirements
1. **Create Issue**: Users can create new issues with a title, description, status, assignment, and priority.
2. **View Issues**: Users can view a list of all issues.
3. **Search Issues**: Users can search issues by title, description, status, assignment, or priority.
4. **Edit Issue**: Users can edit existing issues.
5. **Delete Issue**: Users can delete issues.
6. **View Issue Details**: Users can view detailed information about a specific issue.

## Technical Requirements
1. **Entity Framework Core**: Used for database operations.
2. **Repository Pattern**: Used for data access logic.
3. **Unit of Work Pattern**: Used for managing transactions.
4. **Dependency Injection**: Used to manage dependencies.
5. **ASP.NET Core MVC**: Used for building the web application.
6. **Unit Testing**: xUnit and Moq for unit tests.
7. **Integration Testing**: xUnit and in-memory database for integration tests.

## Setup and Installation
1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/IssueTracking.git
    cd IssueTracking
    ```

2. **Setup the database**:
    Update the connection string in `appsettings.json` file if necessary.

3. **Restore the dependencies**:
    ```bash
    dotnet restore
    ```

4. **Apply migrations**:
    ```bash
    dotnet ef database update
    ```

## Running the Application
To run the application, use the following command:
```bash
dotnet run --project Tracking
```markdown
## Running the Tests
To run all the tests, use the following command:
```bash
dotnet test

## Contributing

Contributions are welcome! Please feel free to submit a pull request.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Contact

For any inquiries or feedback, please contact Lino Khan, (mailto:linokhan1@gmail.com).
