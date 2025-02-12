# IdentityManager

IdentityManager is a user authentication and authorization system built with ASP.NET Core. It provides secure management of user identities, role-based access control, two-factor authentication (2FA), and integrates seamlessly into applications requiring robust identity management.

## Features

- **User Management**: Add, edit, delete, and manage user identities.
- **Role-Based Authorization**: Assign and manage roles for controlled access to application features.
- **Secure Passwords**: Hashing and salting implemented for secure password storage.
- **Two-Factor Authentication (2FA)**: Adds an additional layer of security by requiring a second factor for authentication.
- **Flexible Integration**: Easily integrates with existing ASP.NET Core applications.

## Getting Started

### Prerequisites

Ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other database supported by EF Core)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/AmrMohamed001/IdentityManager.git
   cd IdentityManager
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Set up the database:

   - Update the connection string in `appsettings.json` to point to your database.
   - Apply migrations to create the database schema:

     ```bash
     dotnet ef database update
     ```

4. Run the application:

   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001` (or the port specified in the application).

## Usage

IdentityManager can be integrated into your ASP.NET Core application to manage authentication and authorization. Below are some common use cases:

- **User Registration and Management**: Use IdentityManager to handle user creation, profile updates, and deletions.
- **Role-Based Authorization**: Define roles and assign them to users for controlled access to application resources.
- **Password Security**: Take advantage of built-in hashing and salting mechanisms to securely store user passwords.
- **Two-Factor Authentication**: Enhance security by enabling 2FA for user accounts.

## Technologies Used

- **Backend**: ASP.NET Core
- **Database**: Entity Framework Core with SQL Server
- **Authentication**: Integrated ASP.NET Core Identity
- **Authorization**: Role-based authorization

## Folder Structure

```plaintext
IdentityManager/
├── Controllers/      # Handles application logic
├── Data/            # Database context and migrations
├── Models/          # Entities and view models
├── Services/        # Business logic and helper services
├── Middleware/      # Custom middleware
├── Program.cs      # Application entry point
├── appsettings.json # Application configuration
```

