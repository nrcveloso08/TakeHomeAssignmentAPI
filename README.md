TakeHomeAssignmentAPI
API Overview
The TakeHomeAssignmentAPI provides various endpoints for managing products, packaging, and authentication. This API follows RESTful principles and supports structured logging, authentication, and API versioning.
API Endpoints
Authentication
•	POST /api/auth/login 
o	Logs in a user and returns a JWT token.
o	Request Body: { "username": "user", "password": "password" }
o	Response: { "token": "<JWT_TOKEN>" }
User Management
•	POST /api/users 
o	Creates a new user.
Products
•	GET /api/v1/products 
o	Retrieves all products.
•	GET /api/v1/products/{id} 
o	Retrieves a product by ID.
•	POST /api/v1/products 
o	Adds a new product.
•	PUT /api/v1/products/{id} 
o	Updates an existing product.
•	DELETE /api/v1/products/{id} 
o	Deletes a product.
Packaging
•	GET /api/v1/packaging 
o	Retrieves all packaging items.
Highlighted Endpoint: GetProductsWithPackagingAsync()
•	GET /api/v2/products 
o	Returns products along with packaging details.
o	Utilizes the stored procedure GetPackagingHierarchy for efficient queries.
Database
The API uses TakeHomeAssignmentDB, which follows the TakeHomeAssignment Database Design. The schema supports efficient retrieval of:
•	A product and all its packaging levels.
•	Packaging that contains a specific item.
Authentication and Security
•	Authentication: JWT (JSON Web Token)
•	Authorization: Secures endpoints requiring valid JWT tokens.
•	Technology Used: ASP.NET Core Identity, JWT Bearer Authentication
API Versioning
•	The API supports versioning via URL segments.
•	Latest Version: v2
•	Endpoints in v2: 
o	/api/v2/products (Returns products with packaging details)
Logging
The API implements structured logging using Serilog.
•	Logs authentication success/failure, API requests, responses, and database interactions.
•	Logs are stored in: 
o	Console
o	File (Logs/log-.txt)
o	SQL Server table (Logs)
Guide to Running the Application
1.	Open Microsoft SQL Server Management Studio (SSMS).
2.	Create a database named TakeHomeAssignmentDB.
3.	Open the project solution in Visual Studio 2022.
4.	Open Package Manager Console.
5.	Run the following command: 
6.	dotnet ef database update
7.	Start the application using: 
8.	dotnet run
9.	The project will build and provide a URL (e.g., http://localhost:{port}/swagger/).
10.	Open the URL in a browser to access the Swagger documentation.
This guide ensures a smooth setup and execution of the TakeHomeAssignmentAPI.

