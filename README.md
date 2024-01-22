<h2 align="center">Expense Payment System</h2>

![image](https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/c7d609d1-6d06-4bff-b452-945b94f6cf5d)



### :small_blue_diamond: Introduction

The Expense-Payment System is designed to streamline the expense management process for field employees and enable employers to quickly and efficiently approve these expenses. This application allows field personnel to instantly enter expenses and request reimbursements, while providing administrators the ability to monitor, approve, or reject these requests efficiently.
<br/>

### :small_blue_diamond:A Modular and Scalable Project Architecture

---

<img width="1061" alt="Screenshot 2024-01-21 at 21 46 19" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/c8b2bb6b-3726-4146-8f44-dfa414a4a33b">


✓ **Business Logic Project** 

Core Functionalities: Houses all the business rules and logic.

CQRS Implementation: Implements Command Query Responsibility Segregation, separating read and write operations.

Dependency Injection Friendly: Designed for easy integration with DI frameworks for flexibility and testability.

✓ **Data Project** 

Data Management: Handles the storage, retrieval, and manipulation of data.

Integration with ORM: Seamlessly integrates with an ORM like Entity Framework Core for efficient data operations.

Data Integrity Ensured: Maintains the integrity and consistency of data throughout its lifecycle.

✓ **Schema Project** 

Database Model: Defines the database schema with entity models representing the business data.

✓ **Web API Project** 

API Endpoints: Serves as the interface for client-server communication.

Secure Access: Implements security protocols, including JWT for authentication and authorization.

API Documentation: Utilizes tools like Swagger for API documentation and testing.

✓ **Base Project** 

Common Utilities: Contains shared resources, utilities, and base classes.

Cross-Cutting Concerns: Handles cross-cutting concerns like logging, exception handling, and configuration settings.

DI Configuration: Central location for Dependency Injection configurations.

✓ **Unit Test Project** 

Test Suites: Comprehensive unit tests for all components, ensuring robustness and reliability.

Mocking and Test Coverage: Utilizes mocking frameworks and ensures high test coverage for quality assurance.

<br/>

### :small_blue_diamond: Project Structure and Features

----

✓ **User Operations** 

Role-Based Access: The system features two distinct roles: Admin and Personnel.

Expense Entry: Personnel can make expense entries for themselves and view their own entries.

Expense Tracking: Personnel can track the status of their expenses, including rejections with reasons.

Payment Notifications: Upon approval, personnel are notified via email about payment processing.

![image](https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/e1d9265d-1b0b-4013-9f90-f4d52c238bfe)


![image](https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/0663d970-4c5f-46f1-94e4-3386938ead60)

<img width="1018" alt="Screenshot 2024-01-21 at 20 09 54" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/d6aa64e1-ea3b-4c76-88bc-da1d0a66b348">




✓ **Report Module**

Personal Expense Reports: Personnel have the capability to generate and download their transaction reports in Excel format.

Company-Wide Reporting: Admins have access to daily, weekly, and monthly spending reports for all personnel, with detailed insights into approved and rejected expenses.

<img width="1447" alt="Screenshot 2024-01-22 at 08 43 57" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/2857dc63-696d-47ef-8e3e-360d217e60eb">

![image](https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/71f23584-0ee7-4dcb-949e-85dbc7383a1d)

<img width="520" alt="Screenshot 2024-01-22 at 08 49 03" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/77c1abd6-c2b3-4f58-a45e-f11c83b4deef">

![image](https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/07c508f3-acaf-41cb-afa7-5f3121402bca)

<img width="427" alt="Screenshot 2024-01-22 at 08 49 38" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/89a56080-0e0f-4cd9-911e-8b0c6b8f045c">




✓ **Expense Transactions**

Detailed Expense Submission: Personnel can submit expenses with detailed categorization, including receipts or invoices.

✓ **Administrator Operations**

Initial Setup: The system is initialized with a minimum of two admin users.

User Management: Admins manage personnel profiles.

<br/>

### :small_blue_diamond: Technology Stack

---

- **Authentication and Security**

JWT (JSON Web Token): JWT provides a secure authentication and authorization method for web applications and APIs.

<img width="1424" alt="Screenshot 2024-01-22 at 08 44 29" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/5e3e7830-fae0-4a88-824c-e560a10dae48">

<img width="1424" alt="Screenshot 2024-01-22 at 08 44 29" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/1de61768-c8b8-4c04-a3a6-535e0304be3d">

- **Coding and API Development**

MediatR: Mediator  implementation for CQRS pattern.

ORM: Entity Framework Core

AutoMapper: Object-to-object mapping.
 
FluentValidation: Data validation

- **Test and Mocking Libraries**

Microsoft.EntityFrameworkCore.InMemory: In-memory database for testing.

Moq: Mocking library for unit tests.

xunit: Unit testing tool. 

FluentAssertions: Fluent API for asserting test results.

<img width="995" alt="Screenshot 2024-01-21 at 14 53 48" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/6e98afb0-4335-46b3-96a3-1ec29ce06334">


- **Utilities and Data Processing**

LinqKit: Enhances LINQ queries.

Newtonsoft.Json: JSON serialization and deserialization.

ClosedXML: Excel file manipulation.

- **Caching**

Redis: A high-performance, key-value based cache and message broker.

Microsoft.Extensions.Caching.Memory: Provides in-memory caching for .NET Core applications.

- **Logging and Monitoring**

Serilog: Advanced logging for ASP.NET Core.

- **Background Tasks and Scheduled Jobs**

Hangfire: Background job processing.

<img width="1188" alt="Screenshot 2024-01-22 at 08 42 43" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/0cb56ffe-d92d-4258-9323-31e30183d7b8">

<img width="1188" alt="Screenshot 2024-01-22 at 08 42 43" src="https://github.com/cigdemcakir/FinalCase-Patika/assets/102484836/c50c9666-5bd9-40a7-b1e4-6a3266717a29">



<br/>
