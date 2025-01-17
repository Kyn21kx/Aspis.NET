# Aspis.NET

## The Purpose
Aspis is a .NET framework for C# web API applications that aims to provide a simple yet powerful errors as values system to avoid having unexpected responses from your backend.

## Features
On top of the main error handling, Aspis boosts your development speed by including several features that are common in Backend development and saves you the time to implement them in doing so.

### Results and error handling

Write your code in a testable, result-based manner by using our `ApiOperationResult` type and process them directly into HTTP responses with the built in `ApiOperationHandler`

Controller logic with result based service:

```csharp
[HttpPost("register")]
public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
{
    ApiOperationResult<User> result = await this.userService.RegisterAsync(userDto);
    //Returns an IActionResult that maps to your specified result / error
    return ApiOperationHandler.Handle(result);
}
```

### Auto map results for Unit testing with Http responses

The definition of an Aspis `ApiOperationResult` contains a `Status` and an optional `ErrorMessage`, the `Status` gracefully maps to Http status codes, whilst still letting you know exactly what happened inside your functions when developing / testing.

The status type is an enum defined as:

```csharp
	[Flags]
	public enum ApiOperationStatus : uint {
		None = 0u,
		Success = 1u,
		Created = 2u,
		Updated = 3u,
		/// <summary>
		/// The input of a process was not in the correct format
		/// </summary>
		ValidationError = 4u << 0,
		/// <summary>
		/// Tried to fetch data that does not exist in the Database
		/// </summary>
		EntityNotFoundError = 4u << 1,
		/// <summary>
		/// Tried to post data, but it currently exists in the Database
		/// </summary>
		DataConflictError = 4u << 2,
		/// <summary>
		/// A user tried to access a resource that they're not allowed to / they have incorrect credentials
		/// </summary>
		AuthorizationError = 4u << 3,
		/// <summary>
		/// Some behaviour internally in the program did not go as expected
		/// </summary>
		InternalError = 4u << 4,
		UnkownError = 4u << 5,
		IsErrorStatus = ValidationError | EntityNotFoundError | DataConflictError | AuthorizationError | InternalError | UnkownError
	}
```

### ORM and Repository implementation

Aspis comes with a built-in repository pattern implementation based off of EF Core, making it compatible with any database provider.

```csharp
public class UserService {
    //Dependency injected repo
    private readonly IRepository<User, int> userRepository;
}
```

The repository implementation is ready to go with the following Database Related methods.
| Method                                         | Description                                               |
|------------------------------------------------|-----------------------------------------------------------|
| `T Insert(T entity)`                           | Inserts a new entity.                                     |
| `Task<T> InsertAsync(T entity)`                | Asynchronously inserts a new entity.                      |
| `IQueryable<T> GetAll()`                       | Retrieves all entities.                                   |
| `Task<IQueryable<T>> GetAllAsync()`            | Asynchronously retrieves all entities.                    |
| `T? FirstOrDefault(PK id)`                     | Retrieves the first entity matching the given ID or null. |
| `Task<T?> FirstOrDefaultAsync(PK id)`          | Asynchronously retrieves the first entity matching the given ID or null. |
| `void Delete(T entity)`                        | Deletes an entity.                                        |
| `Task DeleteAsync(T entity)`                   | Asynchronously deletes an entity.                         |
| `T Update(T entity)`                           | Updates an existing entity.                               |
| `Task<T> UpdateAsync(T entity)`                | Asynchronously updates an existing entity.                |
| `Task InsertManyAsync(ICollection<T> entities)` | Asynchronously inserts multiple entities.                 |
| `Task<IQueryable> UpdateManyAsync(ICollection<T> entities)` | Asynchronously updates multiple entities.                 |
| `Task DeleteManyAsync(ICollection<T> entities)` | Asynchronously deletes multiple entities.                 |
| `IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)` | Retrieves all entities including specified related properties. |
| `Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors)` | Asynchronously retrieves all entities including specified related properties. |
| `R Query<R>(Func<IQueryable<T>, R> queryMethod)` | Executes a custom query on the entities.                  |
| `Task<R> QueryAsync<R>(Func<IQueryable<T>, R> queryMethod)` | Asynchronously executes a custom query on the entities.   |

### Plug-n-play Database context
One of the most annoying things in backend development with C# is having to setup everything needed to work with a database, specially the Database Context. Aspis provides you with the default `AspisDbContext` which comes prepared to work with our repository implementation out of the box. You only need to add the `DbSet` for any new entity coming into the database.

