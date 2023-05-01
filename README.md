
# Tulip HR Web Application

This API allows an Admin to manage an organization's hierarchy by viewing, adding, removing, and updating positions and employees associated with those positions. The API can be used to view the organization hierarchy in a clear format that shows the hierarchy, position titles and numbers, and employee details.

## Getting Started

To get started with TulipHR, follow these steps:
### Prerequisites

- .NET Core SDK 6+
- Visual Studio 2022
- Postman or any other API client
### Installation
Once you have these prerequisites installed, follow these steps:
- Clone the repository.

```bash
git clone https://github.com/Ranjitdhaliwal/TulipHR.git
```
- Open the solution file TulipHR.API.sln in Visual Studio
- Build the solution to restore NuGet packages and compile the code.
- Run the API by hitting the play button in Visual Studio or by running dotnet run in the terminal.
- To test the API endpoints, use tools like __Postman__ or access the __Swagger UI__ in your browser. 

## Architecture
The application consists of four main components:   
- Controller: This layer handles incoming requests from the client and passes them to the appropriate service for processing.
- Repository: This component is responsible for interacting with the database. It receives input from the BusinessLogic component, saves or retrieves data from the database, and returns the output to the BusinessLogic component.
- Database Context: This component provides an abstraction layer between the Repository component and the underlying database. It exposes a set of APIs that can be used to interact with the database.
- Models: This layer defines the data models used in the application

The application uses two entity classes, __Employee__ and __Position__. 
  - Employee: This entity represents an employee in the company. It contains properties such as __Id, FirstName, LastName, Number, PositionId, and Position__. Position is a navigation property that links the Employee entity to the Position entity.

  - Position: This entity represents a position in the company. It contains properties such as __Id, Title, Number, ManagerPositionId, and ManagerPosition__. ManagerPosition is a navigation property that links the Position entity to the Position entity, representing the manager of the position.

## Deployment 
The TulipHR API is currently hosted on Azure App Service, and its Swagger URL is https://tuliphr.azurewebsites.net/swagger/index.html. You can use this URL to explore the different endpoints and interact with the API.

## CI/CD Pipeline
This project uses GitHub Actions to automatically build and deploy changes to the __main__ branch. When changes are pushed to the branch, the following happens:

- The code is built a using the dotnet build commands.
- Once build is successful, the application is deployed to Azure using dotnet publish and Azure Web App Deployment action.


## API Usage
  - Navigate to the Swagger page: https://tuliphr.azurewebsites.net/swagger/index.html
  - Use the Swagger UI to test the API endpoints

The TulipHR API provides the following endpoints:

### GET /api/employees
Returns a list of all employees in the organization.

### GET /api/employees{id}
Returns a employee with a given ID .

### POST /api/employees
Adds a new employee to the organization.

### PUT /api/employees{id}
Updates an existing employee's information.

### GET /api/positions
Returns all positions in the organization, including their vacancies and associated employees.

### GET /api/positions/{id}
Returns position in the organization, including their vacancies and associated employee.

### POST /api/positions
Adds a new position to the organization.

### DELETE /api/positions/{id}
Deletes an existing position from the organization.

### GET /api/positions/hierarchy
Returns a hierarchical view of positions in the organization, starting from the given position ID. If no ID is provided, returns a view of the entire organization.

To use the API, make HTTP requests to the appropriate endpoints using an HTTP client of your choice, such as Postman or cURL.
