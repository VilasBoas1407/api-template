
# .Net API Template

This repository contains a .NET API template that uses Clean Architecture, the CQRS pattern, migrations for SQL database setup, and ErrorOr for standardized error handling. It serves as a foundation for building scalable APIs by separating concerns into well-defined layers, promoting maintainability, and providing flexibility in the code.
## Project Structure

The architecture of the project is divided into the following layers:

#### API (Interface Layer)
Contains the API controllers responsible for exposing routes and handling HTTP requests.
Includes HTTP pipeline configuration, authentication, and other integration settings.
#### Application (Application Layer)
Contains the use cases of the application.
Each use case is implemented as a command or query, promoting the separation of reading and writing responsibilities.
Responsible for orchestrating the business logic between domain objects and API requirements.
Implements the CQRS (Command Query Responsibility Segregation) pattern.
#### Contracts (Contracts Layer)
Contains interfaces and DTOs (Data Transfer Objects) used by the layers above.
Defines the communication contracts between the application and infrastructure layers.
#### Domain (Domain Layer)
Contains entities, aggregates, domain services, and validation logic.
Focuses on the data model and business rules, independent of any persistence technology.
All core business logic resides here.
#### Infra (Infrastructure Layer)
Implements data persistence and integrations with external technologies (e.g., database, third-party services).
Contains concrete implementations of the interfaces defined in the Contracts layer.
Includes database context and migrations setup for SQL database.


## CQRS
The CQRS pattern is used to separate reading and writing operations:

Commands: Used for operations that change the state of the system, such as creating, updating, or deleting data.
Queries: Used for retrieving data without altering the system's state.
This approach promotes better scalability by allowing the read side to be optimized separately from the write side.

## ErrorOr for Standardized Results
In order to handle errors and responses in a uniform way, this template uses the ErrorOr package. ErrorOr provides a clear and consistent way to return success or failure results, helping to manage errors without relying on exceptions.

How ErrorOr Works:
Success Response: If the operation is successful, the result will contain a Success status with the desired data.

Error Response: If the operation fails, the result will contain an Error status along with a collection of error details (such as error messages, error codes, etc.).

In both cases, the ErrorOr object is returned from service methods, and it is up to the controller or calling code to handle the result accordingly.

Example:

``` C#
public Task<ErrorOr<UserDto>> GetUserAsync(Guid userId)
{
    var user = _userRepository.GetUserById(userId);
    if (user is null)
    {
        return Task.FromResult(ErrorOr<UserDto>.FromError(new NotFoundError("User not found")));
    }

    var userDto = _mapper.Map<UserDto>(user);
    return Task.FromResult(ErrorOr<UserDto>.FromSuccess(userDto));
}
```

In this example, the method either returns a UserDto wrapped in a successful ErrorOr or an error with details if the user is not found.
## Running the Project

Clone the repository

```bash
  git clone https://github.com/VilasBoas1407/api-template
```

Navigate to the project folder

```bash
  cd api-template
```

Restore NuGetPackages

```bash
  dotnet restore
```

Update your database
You need to have a SQL Server instance and configure the connection string on appsettings.


```bash
  dotnet ef database update
```


## Technologies Used
- .NET 6/7
- ASP.NET Core
- CQRS
- Clean Architecture
- Entity Framework Core
- FluentValidation
- SQL Database (with migrations)
- ErrorOr (for standardized result handling)
## Used for

This project is a base project for me start new apis

- auth-api 

## License
This project is licensed under the 
[MIT](https://choosealicense.com/licenses/mit/)


