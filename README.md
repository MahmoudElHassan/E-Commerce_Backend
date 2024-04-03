# ASP.Net Core API
* [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
* [Entity Framework 8.0](https://learn.microsoft.com/en-us/aspnet/entity-framework)
* [SQL Server 18](https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16)
* [Git](https://git-scm.com/)

## Projects Instructure is **Repository Pattern**
### API Layer: *Receive HTTP Request and Responded*
* Serving static content from APIs
* Validation Data and Error:
  * _Data annotation:_ Add validation attribute to data we receive from the client to ensure our API doesn’t accept bad data and returns the model state errors to the client
* Specification Pattern:
  * Sorting
  * Filtering
  * Pagination
  * Searching
  

### Business Layer: *Communicate with DB send queries and get data*
* Shaping Data
* AutoMapper DTOs
* ASP.NET Identity: 
  * Context Boundaries
  * Using the UserManager and SignInManager
  * JWT token
    
* Add Redis to store the Basket:
  * In-memory data structure store
  * Supporting strings, hashes, lists, etc
  * Key/Value store
  * Fast
  * Persists data by using snapshots every time
  * Data can be given time to live
  * Best way to cache data
  
* Stripe Account:
  * Checkout a Stripe account with strong customer authentication 
  * Validate cards and confirm card payment and Webhook 
  * Stripe working is:
    * After creating Order API if successful make payment to Stripe
    * Stripe returns a one-time use token if payments succeed 
    * The client sends token to the API
    * API variety token with Stripe
    * Stripe confirm token
    * on success/failure, results are sent to the client from the API



### Database Layer: *Contain the business Entities and don’t depend on anything else*
  * Generic Repository:
    * Avoid duplicate code
    * Type Saftey
    * Using  it rather than creating them
  * IQueryable<T> creates an expression tree 
    * Order Entity:
    * Aggregate and Owned Entities
    * Unit of Work Entities 
