# ECommerceService.API

## Overview
ECommerceService.API is an API project built on .NET 8.0. It provides a robust backend for managing e-commerce functionalities such as user authentication, role management, product catalog, order processing, and payment integration. 

## Features
- **User Authentication and Authorization**: Supports JWT Bearer authentication and role-based access control.
- **Role Management**: Admins can manage user roles and permissions.
- **Product Management**: CRUD operations for products and categories.
- **Order Management**: Handles order creation, updates, and tracking.
- **Email Notifications**: Sends email notifications for various events like user registration, password reset, etc.
- **Payment Integration**: Integrates with PayStack payment gateway for processing payments.
- **API Documentation**: Swagger integration for API documentation.

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server 
- HangFire (for background jobs and scheduling)
- SMTP server (for sending emails)

### Installation
1. **Clone the repository**
    ```
   git clone https://github.com/Promise30/ECommerceService.API.git
   ```

2. **Restore dependencies**:
a. Open a terminal and navigate to the project directory.
b. Run the following command to restore the dependencies:
    ```
    dotnet restore
    ```
c. Modify the configuration in appsettings.Development.json to your personal configuration details
 
3. **Update the database**:
a. **Update the database connection string**:
b. Open the `appsettings.json` file and update the `ConnectionStrings` section with your database connection string.
c. **Run the database migrations**:
    ```
    dotnet database update
    ```

4. **Run the application**:
    ```
    dotnet run
    ```
5. **Access the API documentation**:
  ``` Open a browser and navigate to e.g`https://localhost:5001/swagger/index.html`.```
