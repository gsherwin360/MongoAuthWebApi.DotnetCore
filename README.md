# MongoAuthWebApi.DotnetCore

In this demo api, I've implemented a secure user registration and login using MongoDB as the data store. Built on the `AspNetCore.Identity.MongoDbCore library`, this API introduces key features, such as user management, JWT-based authentication, and user lockout capabilities.

## Key Features

- **Secure User Registration and Login**: Users can securely register and log in to their accounts.

- **JWT-Based Authentication**: Enhanced security with JSON Web Token (JWT) authentication.

- **User Management with MongoDB**: Leverage MongoDB as the data store for efficient user management.

- **User Lockout Functionality**: Enhance security by implementing user lockout features. In this API, a user's account is temporarily locked after five consecutive failed login attempts. The account remains locked for five minutes, after which the user can attempt to log in again. This feature helps against unauthorized access and brute force attacks.

- **API Documentation with Swagger**: Easily explore and interact with API endpoints using Swagger documentation.

## Development Prerequisites

Before you start developing with this project, ensure that you have one of the following development environments:

- Visual Studio 2022 (IDE) / Visual Studio Code (Source-code editor)
- .NET 7.0 SDK (for building and running the project)
- MongoDB Compass (or another MongoDB client for local development and testing)
- MongoDB (for the backend database)
