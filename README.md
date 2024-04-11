# MongoAuthWebApi.DotnetCore

In this demo API, I've implemented secure user authentication and management using MongoDB as the database. The API is built on the `AspNetCore.Identity.MongoDbCore` and introduces key features such as user registration, JWT-based authentication, and user lockout capabilities.

## Key Features
- **Secure User Registration and Login:** Users can securely register and log in to their accounts.
- **JWT-Based Authentication:** Enhanced security with JSON Web Token (JWT) authentication.
- **User Lockout Functionality:** Enhanced security by implementing user lockout features. In this API, a user's account is temporarily locked after five consecutive failed login attempts. The account remains locked for five minutes, after which the user can attempt to log in again. This feature helps against unauthorized access and brute force attacks.

## Development Prerequisites
Before diving into development with this project, ensure you have the following prerequisites:
- **Development Environment:** Either Visual Studio 2022 (IDE) or Visual Studio Code (Source-code editor)
- **.NET 7:** Required framework version for building and running the project
- **MongoDB:** Required for the database
- **MongoDB Compass:** A graphical user interface for MongoDB, helpful for data visualization and management

## Getting Started
To run the API locally, follow these steps:
1. Clone this repository to your local machine.
2. Ensure you have MongoDB installed and running.
3. Open the project in your preferred development environment.
4. Configure your MongoDB connection string in the `appsettings.Development.json` file. 
5. Build and run the project.
   
Once the API is running, you can access its documentation to explore and interact with the available endpoints. If you're using Visual Studio, Swagger documentation is automatically loaded. For VS Code, you can navigate to [Swagger](http://localhost:5281/swagger/index.html) in your web browser while the API is running.
