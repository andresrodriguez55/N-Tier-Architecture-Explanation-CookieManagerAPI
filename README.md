<br>WARNING: This is an excerpt from a post from my blog, the images may not load properly here, you can see the same content correctly by clicking <a href="https://andresrodriguez55.github.io/#/post/72/Internship%20Project%20-%20Cookie%20Manager%20API%20Implementing%20N-Tier%20Architecture%20with%20.NET%20Core,%20CI-CD%20Process%20Control%20&%20Docker%20Export"> here. </a> </br>

# 1. Database
## 1.1. Design

![](https://drive.google.com/uc?id=1K_HzvYO_JMojQJfhu2Mh9u5E5aQpVO4I)

The reason why there is an id primary key in the website table is made to distinguish records, some pages may have subdomains in this table, so the domain attribute is not selected as the primary key, the relevant attribute will contain the domain and subdomain information. In addition, an attribute to store the creation date has been added to each table. After choosing the most appropriate option, first all the necessary attributes were added, then the API was implemented over .NET Core.

## 1.2. Local Server Settings

PostgreSQL is used as the database management system in the project, administrator permissions are required to establish new server connections in the relevant tool, administrator permissions are limited on the company computer used. When the preparations for the project were made, the subject was mentioned in the first interviews with the mentor, the mentor mentioned that instead of setting up a server, I could set up the server through Docker to learn how instant corporate companies prepare projects in Docker environment.

After providing the Docker institution, server and empty database installation processes were carried out.

```bash
docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -d postgres
```

As seen above, a similar command is used on the command line.

# 2. N-Tier Architecture

![](https://drive.google.com/uc?id=1AsEnMjrBlGrNAelZ9W8BZSnU4KOQd_ux)

The project was carried out in accordance with the agile methodology. After determining the database ER diagram, an architectureless Rest API was quickly implemented with .NET Core, these stages were carried out in the first sprint.

In the second sprint, the N-Tier architecture was adjusted, while editing, classes that did not have additional required snapshots were also included. Let's examine the result of the project in the .NET Core environment, first let's examine the file hierarchy.

## 2.1. Data Layer

![](https://drive.google.com/uc?id=1fRCiLRGFNbNJ_dMqD6zbwzPIWgDBPxZm)

In the data layer, there will be classes that will be related to the data, these are the classes of entities in the database, the class that will connect to the database, and the classes that will provide CRUD operations for each entity. This architecture gives the chance to include different technologies easily, to explain this more clearly, we need to examine the classes of projects and projects in this layer first. Since the file hierarchy is preserved, we can examine the files found in the projects, let's talk about the task performed by each class.

### 2.1.1. Data Project

![](https://drive.google.com/uc?id=1Cj4TvxGCK9-ANZ0f2s9DwU11eEytR4t4)

The purpose of this project is to establish the data connection only, if a database is used, to create the tables of the relevant database and to provide CRUD operations. Let's talk about the files and the classes in the files in order to explain clearly.

Context file will hold context classes, these classes will provide database connections, thanks to this hierarchy, context separation can be done easily and S.O.L.I.D. It ensures easy additions by preserving its principles. For example, a new context can be easily added here, this context can provide an InMemory database creation, not a connection to a database.

To understand the DataAccessRegistration class in the DataAccessRegistration file, it is first necessary to remember the N-Tier hierarchy.

![](https://drive.google.com/uc?id=1AmLMDYPboO7EYP0MOU8_-ZuFYovM5rR9)

Layers are built on top of each other, a chain is formed, each top layer needs the bottom layer. In OOP concepts, there is a dependency injection in order to realize this dependency in the most accurate way (class principles and the most accurate isolation between classes). Our aim here is to make the objects as independent as possible. Returning to our situation, we need to understand briefly that our business layer will need our data layer, our business layer will either forward or receive data to the data layer by controlling our business rules, and it will provide this with the repository classes (classes that provide CRUD operations) in the data layer. In order to isolate this dependency in the most accurate way and considering that there is a possibility of expansion of the project, we prevent it with the use of interfaces, we will talk about this point in the next chapter, to put it briefly here, we have derived our repository from interfaces, considering that the use of new technologies may exist, while the business layer is directly Instead of getting repositories, they will handle objects of the corresponding interfaces type. The DataAccessRegistration class, on the other hand, allows us to set the object types of the repository to correspond to the interfaces, in short, it allows us to choose the object derived from the interface to be used by default for each interface. Let's see the contents of the class for better understanding.

```csharp
using KariyerNet.CookieManager.Data.Contract.Repository;
using KariyerNet.CookieManager.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace KariyerNet.CookieManager.Data.DataAccessRegistration
{
    public static class DataAccessRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IWebSiteRepository, WebSiteRepository>();
            services.AddScoped<IWebSiteCookieTypeDefinitionRepository, WebSiteCookieTypeDefinitionRepository>();
            services.AddScoped<ICookieRepository, CookieRepository>();

            return services;
        }
    }
}
```

The AddScoped method creates new instances specific to each request, but each created instance within the scope of each request is run as a singleton. It has been made static in order not to make the class a new instance from the outside.

The classes in the Mappings file contain the rules of the entities that will take place in the data layer. These rules can be written in entity classes, but are written separately to maintain code readability and comprehensibility, thus preserving the roles of the classes.

The classes in the Migrations file are used to create the entities written with the Code First approach and the rules of the entities in the database, these classes are created through the entity framework, the classes are obtained by typing the relevant commands on the command line, and these classes are used to create and update the database in the database.

The classes in the repository file will help us perform CRUD operations on the data layer, let's examine the code of the CookieRepository class to better understand the interface usage issue we mentioned earlier.

```csharp
using CookiesSettings.Models;
using KariyerNet.CookieManager.Common.Data;
using KariyerNet.CookieManager.Data.Context;
using KariyerNet.CookieManager.Data.Contract.Repository;

namespace KariyerNet.CookieManager.Data.Repository
{
    internal class CookieRepository : GenericRepository<Cookie, int>, ICookieRepository
    {
        public CookieRepository(CookieSettingsContext context) : base(context)
        {

        }
    }
}
```

Here, let's examine the important cases in order. First of all, note that this class is an internal class, which means it is accessible in the layer it is in, remember that our main purpose is to isolate classes as well as dependencies and preserve their principles, so it is an internal class.

The class is a subclass of the GenericRepository class, it also implements the ICookieRepository interface, its constructor takes a context, it has a context variable that it uses in common with GenericRepository subclasses, the context taken in the constructor will replace this variable, so with the base keyword We call the constructor in the super class and thus we set the common variable. We will examine the related super class and interface later, so we will not go into further details. Finally, it should be said that these applied class distinctions allow the addition of new methods easily. To better understand this, let's examine the UML diagram that contains only the relevant classes, since I will only use the picture to describe this situation, methods and variables will not be included in the diagram.

![](https://drive.google.com/uc?id=1U45_yjMxd4SrKbxw4Hb6OO8I4FWjH_68)

IGenericRepository interface will have methods to be used in all repository, these methods will be related to CRUD operations. Considering that different technologies can be used in the future, the interface has been created for now. The methods of the GenericRepository class interface will be implemented, since it is working with Entity Framework, EF methods are included in its methods. The CookieRepository class is a subclass of the GenericRepository class, inheriting all the methods of its superclass. ICookieRepository interface implements IGenericRepository interface, here we can write additional methods, then these methods will be implemented by CookieRepository, ICookieRepository helps us to know additional written methods easily, code readability and principles preservation of classes are increased. We'll talk about this again later when we talk about the Common layer.

What we said about the CookieRepository class also applies to other classes, each class has interfaces for writing additional methods, the same logic is preserved.

### 2.1.2. Contract Project

![](https://drive.google.com/uc?id=18rTdRY25yNiIXKoNdvmkv_O6rI6lWeuy)

Here are the interfaces used to create additional methods independent of the generic repository of the repository mentioned in the previous title. Interfaces at each layer are distinguished by contract projects, thus maintaining a clearer hierarchy.

Since there are only simple basic methods that associate the assets in the Rest API application, complex additional methods were not required to be written, therefore, the repository interfaces of all assets are empty.

### 2.1.3. Entity Project

Only used entities are included here. Let's examine the Cookie class as an example to understand the principles that classes have.

```csharp
using KariyerNet.CookieManager.Common.Data;

namespace CookiesSettings.Models
{ 
    public class Cookie : BaseEntity<int>, IHasCreatedDateEntity
    {
        public string SessionId { get; set; }
        public bool Status { get; set; }
        public int WebSiteCookieTypeDefinitionId { get; set; }
        public virtual WebSiteCookieTypeDefinition WebSiteCookieTypeDefinition { get; set; } = null!;
        public DateTime CreatedDate { get; set ; }
    }
}
```

Classes are subclasses of BaseEntity like the Cookie class and implement the IHasCreatedDateEntity interface. We will examine the related classes later, but briefly here, the importance of these classes includes the common variables that should be found according to the technology to be used in each class. The BaseEntity class is a generic type class because there is a primary key in the database tables, so the primary key data type can be determined easily. The IHasCreatedDateEntity class is the class that will solve this situation because it is desired to know when each entity was created or updated in databases. The class contains the get and set methods of the CreatedDate variable, the entity classes will implement these methods.

To understand why the IHasCreatedDateEntity class is needed, we need to examine our context class.

```csharp
using CookiesSettings.Models;
using KariyerNet.CookieManager.Common.Data;
using KariyerNet.CookieManager.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KariyerNet.CookieManager.Data.Context
{
    public class CookieSettingsContext : DbContext
    {
        public CookieSettingsContext(DbContextOptions<CookieSettingsContext> options) : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<WebSite> WebSites { get; set; }
        public DbSet<WebSiteCookieTypeDefinition> WebSiteCookieTypeDefinitions { get; set; }
        public DbSet<Cookie> Cookies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CookieMappings());
            modelBuilder.ApplyConfiguration(new WebSiteMappings());
            modelBuilder.ApplyConfiguration(new WebSiteCookieTypeDefinitionMappings());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntity && 
                    (e.State == EntityState.Added || e.State == EntityState.Modified))
                .ToList();

            SetDefaultDateTimeValues(entries);

            var count = base.SaveChanges();
            foreach (var entry in entries) 
                entry.State = EntityState.Detached;

            return count;
        }

        private void SetDefaultDateTimeValues(List<EntityEntry> entries)
        {
            if (entries.Count <= 0) 
                return;

            foreach (var entityEntry in entries)
            {
                if ((entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified) && 
                    entityEntry.Entity is IHasCreatedDateEntity)
                {
                    var createdDate = (DateTime)entityEntry.Entity.GetType().GetProperties()
                        .FirstOrDefault(x => x.Name == nameof(IHasCreatedDateEntity.CreatedDate))
                        .GetValue(entityEntry.Entity);

                    if (createdDate == default) 
                        ((IHasCreatedDateEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
        }
    }
}
```

Note that our context class is used as a subclass of EF's DbContext class. Thus, there will be methods as inheritance and we will also be able to change the behavior of those methods. The SaveChanges method serves to save the changes made in EF, we will check whether the addition or update is made when the relevant method is called. We filter the added or updated entities, after we find the related entities we will send them to the SetDefaultDateTimeValues method. The corresponding method will handle entities derived from the IHasCreatedDateEntity method, i.e. if an entity does not implement the IHasCreatedDateEntity interface, it will ignore it even if it has the CreatedDate variable. Here we will set the CreatedDate variable, thus eliminating the need to do it manually.

## 2.2. Business Layer

![](https://drive.google.com/uc?id=16cWINTbEuZ_UHpg4w98ZruhaTDOqaVMS)

The business layer is built on the data layer, there are classes that will provide CRUD operations for each entity, these classes are the repository classes in the data layer, unlike Data Object Transfer classes are taken as parameters or returned objects, these objects contain variables that will be required for the client. , so we do not get or return all the attributes of the entities, the parameters of the related ODT objects are validated with the validate classes in the addition and update processes, and after validation, the repository classes in the data layer are used. To make these more clear, let's examine each project in the tier.

### 2.2.1. Business Project

![](https://drive.google.com/uc?id=1IRYwDVIIkiY0oDwu9GuHiA30rPIbPk2U)

The classes in the BusinessEngine file will perform CRUD operations by checking our business rules. Let's take a look at the WebSiteEngine class as an example.

```csharp
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSites;
using KariyerNet.CookieManager.Business.Validations;
using KariyerNet.CookieManager.Business.Validations.WebSites;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;
using System.Runtime.CompilerServices;
using KariyerNet.CookieManager.Common.Exceptions;

[assembly: InternalsVisibleTo("NUnitTests")]
namespace KariyerNet.CookieManager.Business.BusinessEngine
{
    internal class WebSiteEngine : IWebSiteEngine
    {
        private readonly IWebSiteRepository _repository;
        public WebSiteEngine(IWebSiteRepository repository)
        {
            _repository = repository;
        }
        public WebSiteListItemDto GetWebSiteById(int id) 
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new BusinessException("Id bulunamadı.");

            var response = entity.Adapt<WebSiteListItemDto>();
            return response;
        }

        public List<WebSiteListItemDto> GetWebSites()
        {
            var data = _repository.GetList();
            return data.Adapt<List<WebSiteListItemDto>>();
        }

        public bool CreateWebSite(WebSiteCreateRequestDto request)
        {
            ValidationTool.Validate<WebSiteCreateValidation>(request);

            var entity = request.Adapt<WebSite>();
            _repository.Create(entity);
            return true;
        }

        public bool UpdateWebSite(WebSiteUpdateRequestDto request)
        {
            ValidationTool.Validate<WebSiteUpdateValidation>( request);

            var entity = request.Adapt<WebSite>();
            _repository.Update(entity);
            return true;
        }

        public bool DeleteWebSite(int id) //farklı metoda bağımlılık var
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new BusinessException("Id bulunamadı.");

            _repository.Delete(entity);
            return true;
        }
    }
}
```

The class is of type internal, it cannot be accessed from different layers. When the project is compiled, the assembly adjustments are made so that the NUnitTests project can access this class. The reason for this is that the CRUD operations of such an architecture are completely dependent on the business layer, the relevant layer validations and the necessary exceptions will be returned, so only this layer will be tested in unit tests, so business engine classes should be visible in the tests.

The class implements the IWebSiteEngine interface, again we have taken the same approach to protect class tasks and increase code readability. The class is dependent on the website repository, we do the repository injection via the constructor.

Pay attention to the methods that perform add and update functions, they take DTO objects as parameters, as we said before, we remove the need to get all the attributes that will be included in the assets by the user, for example, if an id primary key in the database uses the identity feature, it is not necessary to get an id from the user or the creation of the entity. or an attribute such as the update date should not be taken. The DTOs received in these methods are audited whether they comply with the business rules, we will talk about them in the following headings.

In Get methods, assets are taken from the repository, notice that the DTO is returned. As we said before, the user may not want to know all the information of the assets or there may be information that needs to be hidden from the user, so before returning the assets, they are translated into DTOs, this process is done automatically via mapster instead of manually.

Note that in the relevant methods, if there is a business error in the received data or if a searched entity is not in the database, a special exception is returned, this is to avoid the idea of ​​adding a different behavior to the application layer for every negative situation, then it will be seen when the relevant layer is reached. No other task has been added other than the task of serving clients, because it complies with S.O.L.I.D.'s Single Responsibility principle. More details on this subject will be given in the future.

There is a BusinessServiceRegistration class in the ServiceRegistration file, it is similar to the DataAccessRegistration class that we have seen in the data layer before, in the BusinessServiceRegistration class, when the existing interfaces in the business engin are taken as parameters in the application layer, it is determined which interfaces derived classes will be used, so that a newly written class can easily make a code change in the BusinessServiceRegistration class. can be integrated. Here, the logic of creating new instances specific to each request and applying the singleton pattern for each created instance within the scope of each request is used.

In the validations file, there are validation classes for the received DTOs in the add and update methods found for each entity in the project, a file hierarchy is preserved with the files having the names of the entities.

![](https://drive.google.com/uc?id=12_Andphq3pB0vWneV04DhvSe3LEwLs4c)

First, let's examine the CookieCreateValidation class as an example.

```csharp
using FluentValidation;
using KariyerNet.CookieManager.Business.Dto.Cookies;

namespace KariyerNet.CookieManager.Business.Validations.Cookies
{
    internal class CookieCreateValidation : AbstractValidator<CookieCreateRequestDto>
    {
        public CookieCreateValidation()
        {
            RuleFor(x => x.SessionId).NotEmpty();
            RuleFor(x => x.Status);
            RuleFor(x => x.WebSiteCookieTypeDefinitionId).NotEmpty();
        }
    }
}
```

FluentValidation is used to provide data validations, the CookieCreateValidation class implements the AbstractValidator abstract class, this abstract class provides FluentValidation, the related class is a generic class, it contains methods to validate the parameters of the objects, we use the related methods in the constructor of the CookieCreateValidation class.

To say why FluentValidation is an ideal solution in our application, the reason is that it allows us to preserve the purpose of the classes, if we wrote the validations in the classes of the entities (i.e. only the database constraints remained) in the future, constraint updates would have to be made to the database when new assumptions were made, and this could cause multiple problems. If validations were written to the engine classes in the business layer, there would be multiple if blocks, which would be against S.O.L.I.D., because our goal is always to preserve the purpose of the methods and preserve their properties, keeping them from being modified. FluentValidation also offers the chance to add our core complex rules.

As for how to integrate classes that will perform CookieCreateValidation and similar constraint functions, the ValidationTool class does this, let's examine the relevant class.

```csharp
using FluentValidation;

namespace KariyerNet.CookieManager.Business.Validations
{
    public static class ValidationTool
    {
        public static void Validate<T>(object entity) where T : class, IValidator, new() 
        //AbstractValidator<T> -> IValidator
        {
            var validator = new T(); 

            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if(!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
```

There is only one method in our ValidationTool class, this method is in the IValidator interface, we implement the method here. Validate method is a generic method, it takes any type of object as a parameter, our DTOs will take place in these parameters. In the remaining lines of code, validation rules written for each object are checked, if the DTO does not comply with any rules, a ValidationException will be thrown.

### 2.2.2. Contract Project

![](https://drive.google.com/uc?id=1C2Lp9n5SDgr0CPKXu9dAiyESTBcspchC)

As we saw earlier in the data layer, we use interfaces to easily integrate new classes, this helps us write new engines that can be used, as well as S.O.L.I.D. obeys the open closed principle of the principles, since the methods to be used are determined at the interfaces, the classes that implement these interfaces will have to implement the corresponding methods, if in the future a class wants to implement only a subset of the methods, the convenience situation can be taken care of, S.O.L.I.D. According to the interface segregarion principle of the principles, interfaces will be broken down and new classes will be able to implement appropriate interfaces.

### 2.2.3. Dto Project

![](https://drive.google.com/uc?id=1PlP48SoRVxc800myaBXKBWZi8k9DxecP)

DTOs used in business engines are located here, they are used to show the information to be shown to the user and to get the expected information from the user, these objects allow us to return the values of any desired entities, for example CookieListItemDto returns an inner join result.

```csharp
public class CookieListItemDto
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public bool Status { get; set; }
        public int WebSiteCookieTypeDefinitionId { get; set; }
        public string WebSiteCookieTypeName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
```

The WebSiteCookieTypeName attribute belongs to the WebSiteCookieTypeDefinition entity. To access this information, only the repository of the cookie entity is not used in the engine class.

## 2.3. Common File/Project

![](https://drive.google.com/uc?id=14iZa5qZhogveku2DDx-t7dJi6HNOug_w)

This structure is not counted as a layer, here are the classes that some layers use in common, note that in fact the classes related to the data layer may be located in their respective layers, normally the Common file contains classes related to third party services, for example connecting to an SMTP server and The class that sent report mails could have taken place. This choice was made because our aim in this project was to learn and master the N-Tier architecture.

BusinessException is an exception that we wrote specially, ErrorHandlerMiddleware class acts as middleware, its task is middleware and when an operation is done in controllers, if an exception occurs, it catches it and returns the exception message to the user.

According to the technologies used, IEntity, BaseEntity and IHasCreatedDateEntity are classes that will help them to be added easily if additional information is required for each entity. Let's examine their code to understand better.

```csharp
public interface IEntity
    {
    }

public abstract class BaseEntity<T> : IEntity where T : struct
    {
        public T Id { get; set; }
    }

public interface IHasCreatedDateEntity
    {
        DateTime CreatedDate { get; set; }
    }
```

IEntity class has root role, as new requirements come new classes will be created and required attributes will be added to related classes and they will implement this interface. BaseEntity specifies the primary key attribute as seen. The IHasCreatedDateEntity class assumes this role because it is desired to know when each row was created or updated in databases.

BusinessException works as a subclass of Exception and implements the constructors of its super class, so we don't need to examine it. Let's examine the ErrorHandlerMiddleware class.

```csharp
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace KariyerNet.CookieManager.Common.Exceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) //asenkron olmalı, birden fazla istek bulunabilir
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BusinessException e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                var result = JsonSerializer.Serialize(new { message = e?.Message });
                await response.WriteAsync(result);
            }

            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = (int)HttpStatusCode.InternalServerError; 

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
```

The class agent will work as a layer, it will run its corresponding task in the try catch block for each called controller method, so we don't need to write try catch blocks in each controller's method. We will be able to distinguish the exception type when the Exception is caught, so we need special exceptions in projects, the message and HTTP code that can be easily returned according to the exception caught can be determined in our class.

```csharp
using KariyerNet.CookieManager.Data.Context;
using Microsoft.EntityFrameworkCore;
using KariyerNet.CookieManager.Business.ServiceRegistration;
using KariyerNet.CookieManager.Data.DataAccessRegistration;
using KariyerNet.CookieManager.Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);

/* The codes in between have been skipped.*/

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

/* The codes in between have been skipped.*/

```

Middleware class is included in our program class.

Let's examine the IGenericRepository and GenericRepository in the data file.

```csharp
public interface IGenericRepository<T, PK> where T : BaseEntity<PK>, new() where PK : struct
    {
        T GetById(PK id); 
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        TResult GetFirstOrDefault<TResult>(Expression<Func<T, bool>> filter) where TResult : class, new();
        TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> filter) where TResult : class, new();
        int RecordCount(Expression<Func<T, bool>> filter = null);
        bool Exists(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes);
        List<TResult> GetList<TResult>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes) where TResult : class, new();
    }

public class GenericRepository<T, PK> : IGenericRepository<T, PK> where T : BaseEntity<PK>, new() where PK : struct
    {
        private readonly DbContext _context;
        protected DbSet<T> DbSet { get; }

        public GenericRepository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public T GetById(PK id)
        {
            return DbSet.FirstOrDefault(e => e.Id.Equals(id));
        }

        public void Create(T entity) 
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return (filter == null) ? DbSet.Any() : DbSet.Any(filter);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return DbSet.FirstOrDefault(filter);
        }

        public TResult GetFirstOrDefault<TResult>(Expression<Func<T, bool>> filter) where TResult : class, new()
        {
            IQueryable<T> query = DbSet;

            return query.Where(filter).ProjectToType<TResult>().FirstOrDefault();
        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            if(filter != null)
                query = query.Where(filter);
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include<T, object>(include);
            if (orderBy != null)
                query = orderBy(query); 
            if (topRecords != null)
                query = query.Take((int) topRecords);

            var result = query.ToList<T>();
            return result;
        }

        public List<TResult> GetList<TResult>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes) where TResult : class, new()
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
                query = query.Where(filter);
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include<T, object>(include);
            if (orderBy != null)
                query = orderBy(query);
            if (topRecords != null)
                query = query.Take((int)topRecords);

            return query.ProjectToType<TResult>().ToList();
        }

        public int RecordCount(Expression<Func<T, bool>> filter = null)
        {
            return (filter == null) ? DbSet.Count() : DbSet.Count(filter);
        }

        public TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> filter) where TResult : class, new()
        {
            throw new NotImplementedException();
        }
    }
```

Here again we see the importance of using interfaces. The IGenericRepository has the methods you will need (and a few methods for learning purposes). Methods are implemented in the GenericRepository class by adhering to Entity Framework ORM technology, the class can be used for any entity, since basic methods such as search and delete depend on the primary keys of the entities, when this class object is specified, it is necessary to determine the PK type of the entities, it is necessary to obtain the basic written methods at the same time. We do this by creating subclasses for the GenericRepository class, and we specify the PK type there. Well, if we need to answer the question of why we use inheritance, the answer is to easily write new methods to repository classes. Thus, the basic methods cannot be changed and S.O.L.I.D. We follow the open closed principle of the principles, we cannot easily change the related methods, but we can integrate new methods.

The methods are optimized as much as possible, the desired attributes can be selected in the methods that return TResult, so that there will be an efficient use of network and ram, other methods will return all the attributes of the desired assets. There are parameters that will provide filtering, row number selection and sorting in the methods that provide the get function, these will provide the necessary conditions in accordance with the LINQ structure, and will ensure the realization of filters and sorting. At the same time, for example, the GetList method has the includes parameter, it will enable these entities to add the entities belonging to the desired foreign key, thus enabling efficient searches.

EF's savechanges method is called to save the relevant changes at the end of the process into the methods that provide the update, add and delete functions. Remember that when this method is triggered, the date editing operations in our data layer will be activated.

## 2.4. Presentation Layer

![](https://drive.google.com/uc?id=1E267d2e2xdrTdkoX9EgyURfY1ut0VigQ)

There is an API to work in this layer, its only task is to provide the necessary service to the user, data exchange will be provided through the previously examined layers. Each entity has HTTP methods in the Controllers folder, so a different method will work according to the type of request made over a single URL, thus protecting the Rest API architecture. Let's examine the CookiesController class for example purposes.

```csharp
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace KariyerNet.CookieManagerApi.Controllers
{
    [ApiController]
    [Route("api/cookies")]
    public class CookiesController : ControllerBase
    {
        private readonly ICookieEngine _engine;

        public CookiesController(ICookieEngine engine)
        {
            _engine = engine;
        }

        [HttpGet()]
        public List<CookieListItemDto> GetCookies()
        {
            return _engine.GetCookies();
        }


        [HttpPost()]
        public bool CreateCookie(CookieCreateRequestDto cookie) 
        {
            return _engine.CreateCookie(cookie);
        }

    }
}
```

Route attribute is used to determine which path (URI) the class will be activated. The class is a subclass of the ControllerBase class. It is expected that an engine will be given through dependency injection in the class constructor, we will not go into details because we have examined this situation in the business layer enough, we just want to remind you that a class derived from the relevant interface type is expected in the constructor, and classes with new technologies and new functionality can be easily included. As we said before, this layer does not have a new function, it is completely dependent on the business layer, as can be seen in the methods provided, it is seen that the methods in the business layer are completely used. DTOs are used for post and put operations.

# 3. Exporting the Project with Docker

It was difficult to export the database, it was seen that most of the people on the internet did them in different ways, most of the results were seen to lead to manual installations and they were not liked, deep researches were made, exporting the .NET project was handled at the end of the researches because it was simple.

As a result of the research, the required commands were learned, the strategy to be applied was determined, let's examine the learned commands.

```bash
REM View images found on the device;
docker images

REM Viewing the containers on the device;
docker ps 

REM View active containers on the device;
docker ps -a

REM Running a container;
docker run -it --name container-name image-name:image-tag

REM Running an image;
docker exec -it image-name:image-tag 

REM Deleting a container on the device;
docker rm container-name

REM Deleting an image on the device;
docker image rm image-name

REM Creating a tar document to export an image;
docker save -o dotnet.tar dotnet-cookiemanager

REM Uploading images contained in a tar document to the device;
docker load -i **.tar

REM Uploading an image to the device;
docker build -t imageName .

REM Exporting the script of a postgres database in a container;
docker exec -i cookie-settings pg_dump -U postgres testDb > postgres-backup.sql
```

These commands were used effectively as a result of research. First, let's examine how we export the database. The last command of the commands shown was used, a dockerfile was prepared after the SQL script was created, let's examine the script.

```bash
FROM postgres:11-alpine

ENV POSTGRES_DB=kariyerNetCookieManager
ENV POSTGRES_USER=SECRET_USER
ENV POSTGRES_PASSWORD=SECRET_PASSWORD
ENV PGDATA=/data

COPY postgresBackup.sql /docker-entrypoint-initdb.d/
```

Here we provide the server and database information in the postgres environment, separately we set the script to run when the database is installed, the script will only run once when the image is built, so by default there will be data, tables and conditions. After doing this, the tar document was prepared with the save command. For .NET, a dockerfile was created in the Visual Studio environment, it creates automatically, after running, the image and container are obtained, then saved as an image tar extension document.

After obtaining the tar extension documents, the docker compose structure was prepared, this ensures that the containers are not discrete, a yml extension document was prepared for docker compose, let's examine its content.

```bash
version: '3.4'
 
networks:
  kariyernet-cookiemanager:
    driver: bridge 
 
services:
  dotnet-cookiemanager:
    image: dotnet-cookiemanager:latest
    depends_on:
      - "postgres_cookiemanager"
    ports:
      - "3000:80"     
    environment:
      DB_CONNECTION_STRING: "host=postgres_cookiemanager;port=5432;database=kariyerNetCookieManager;username=SECRET_USER;password=SECRET_PASSWORD"
    networks:
      - kariyernet-cookiemanager  
  
  postgres_cookiemanager:
    image: postgres_cookiemanager:latest
    ports:
      - "5432"
    restart: always
    environment:
      POSTGRES_USER: "SECRET_USER"
      POSTGRES_PASSWORD: "SECRET_PASSWORD"
      POSTGRES_DB: "kariyerNetCookieManager"
    networks:
      - kariyernet-cookiemanager
```

Here, the containers that will work are collected, we specify which images they will work on and which ports they will work on.

Now, in order to run applications, only tar extension documents need to be loaded, then the docker compose document must be run with the “docker-compose up” command, there are no additional steps, it is tried to avoid the rather manual installation situation, especially for the database issue, most people on the internet just export the sql script It states that it is sufficient to install it, but this increases the steps to be taken by the person who will install it. Alternatively, it is said that the volume files can be detected and embedded in the images, but this is not much different from the solution we have applied. Storing the script as a backup offers advantages, it can also be used as a recovery point.

# 4. Unit Tests

![](https://drive.google.com/uc?id=1Yf_i73WqKvfmOvRUITta4zvzWrCh4rCd)

Tests were carried out easily due to the use of a layered architecture. The tests are completely aimed at the business layer, remember that all the validations in our application are made by the business layer, while our application layer is focused on using only the relevant sub-layer.

The widely used NUnit Framework was used to write the tests. Let's examine the CookieEngineTest class for example purposes.

```csharp
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.Cookies;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;
using Moq;

namespace NUnitTests.Engines
{
    public class CookieEngineTest
    {
        private readonly Mock<ICookieRepository> _cookieRepository;
        private readonly Mock<IWebSiteCookieTypeDefinitionEngine> _webSiteCookieTypeDefinitionEngine;
        private readonly ICookieEngine _cookieEngine;

        public CookieEngineTest()
        {
            _cookieRepository = new Mock<ICookieRepository>();
            _webSiteCookieTypeDefinitionEngine = new Mock<IWebSiteCookieTypeDefinitionEngine>();
            _cookieEngine = new CookieEngine(_cookieRepository.Object, _webSiteCookieTypeDefinitionEngine.Object);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetCookies_ReturnsAllItems()
        {
            //arrange
            List<Cookie> data = getSampleData();
            _cookieRepository.Setup(x => x.GetList(null, null, null, c => c.WebSiteCookieTypeDefinition)).Returns(data);
            int count = data.Count;

            //act
            var result = _cookieEngine.GetCookies();

            //assert
            Assert.AreEqual(count, result.Count);
        }

        [Test]
        public void CreateCookie_ValidCookie_ReturnsTrue()
        {
            //arrange
            List<Cookie> data = getSampleData();
            Cookie cookie = data[0];

            CookieCreateRequestDto request = cookie.Adapt<CookieCreateRequestDto>();
            _cookieRepository.Setup(x => x.Create(It.IsAny<Cookie>()));

            WebSiteCookieTypeDefinitionListItemDto foreignKey =
               cookie.WebSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionListItemDto>();
            _webSiteCookieTypeDefinitionEngine.Setup(
                x => x.GetWebSiteCookieDefinitionById(It.IsAny<int>())).Returns(foreignKey);

            //act
            var result = _cookieEngine.CreateCookie(request);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateCookie_InvalidCookie_CookieMustBeAccepted_GetsException()
        {
            //arrange
            List<Cookie> data = getSampleData();
            Cookie cookie = data[0];
            cookie.Status = false;

            CookieCreateRequestDto request = cookie.Adapt<CookieCreateRequestDto>();
            _cookieRepository.Setup(x => x.Create(It.IsAny<Cookie>()));

            WebSiteCookieTypeDefinitionListItemDto foreignKey =
               cookie.WebSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionListItemDto>();
            foreignKey.IsRequired = true;
            _webSiteCookieTypeDefinitionEngine.Setup(
                x => x.GetWebSiteCookieDefinitionById(It.IsAny<int>())).Returns(foreignKey);

            //act

            //assert
            Assert.That(() => _cookieEngine.CreateCookie(request), Throws.Exception);
        }

        private List<Cookie> getSampleData()
        {
            List<Cookie> cookies = new List<Cookie>
            {
                new Cookie
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    SessionId = "adfggw2342",
                    Status = true,
                    WebSiteCookieTypeDefinitionId = 1,
                    WebSiteCookieTypeDefinition = new WebSiteCookieTypeDefinition()
                    {
                        Id = 1,
                        CookieType = "performance",
                        Title = "performans",
                        Description = "kullanıcı geçmişinden faydalanarak kullanıcının site performansını artırır",
                        IsRequired = true,
                        IsActive = true,
                        WebSiteId = 4,
                        WebSite = new WebSite()
                        {
                            Id = 4,
                            Name = "unisbul"
                        }
                    },
                },
                new Cookie
                {
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    SessionId = "axadfggw2342",
                    Status = false,
                    WebSiteCookieTypeDefinitionId = 2,
                    WebSiteCookieTypeDefinition = new WebSiteCookieTypeDefinition()
                    {
                        Id = 2,
                        CookieType = "targeting",
                        Title = "...",
                        Description = "........",
                        IsRequired = false,
                        IsActive = true,
                        WebSiteId = 3,
                        WebSite = new WebSite()
                        {
                            Id = 3,
                            Name = "iskolig",
                        }
                    }
                }
            };

            return cookies;
        }
    }
}
```

Testing has not been given much importance, mostly because integration tests are performed on Rest APIs, but this still has not been an obstacle to writing unit tests and obtaining new information.

# 5. Management of CI/CD Processes with Azure Devops Environment

Student mail was used to use the Azure Devops environment, so some additional services related to Azure were examined and the necessary additional services were used.

![](https://drive.google.com/uc?id=1YFdjTwm-yY0jBCjvXvhTBUNSA4b-v0pr)

![](https://drive.google.com/uc?id=110T-4Fd68WLywXRt7vSTkkyp930A735V)

![](https://drive.google.com/uc?id=1zCSyK8iYQffUHOT5No02OgYzEwfXW57e)

![](https://drive.google.com/uc?id=1ep3qH4VqcP-7SNkNGOnJca-CJDrhpt5E)

After the processes, the pipeline was created for the CI process of the application, this pipeline provides the application to install, build and run the NUnit Tests, the relevant pipeline can run automatically when any new code change is made, it helps to control the application very quickly.

![](https://drive.google.com/uc?id=10J1XG2Ul8eOO9bbX80Vp2PVGz_wFyLiN)

After providing CI controls, CD process was started, Azure Web Services was used to release the application.

![](https://drive.google.com/uc?id=1ibKNJJmfrrDhLcTvlkCYY4M3KDEgLF1C)

In addition, it should be noted that there is no need to check database connections in the CD process, the focus is purely on the application, so any data cannot be retrieved.
