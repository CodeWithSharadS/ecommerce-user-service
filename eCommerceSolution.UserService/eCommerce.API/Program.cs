using eCommerece.Infrastructure;     
using eCommerece.Core;
using eCommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);   

builder.Services.AddInfrastructure();

builder.Services.AddCore();

//Add controllers to the service collection
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// for auto mapper
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

// for fluent validation
builder.Services.AddFluentValidationAutoValidation();

// for swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// for cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("").AllowAnyMethod().AllowAnyHeader();
    });
});

//customize the fluent validation response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // Extract errors into a dictionary
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var response = new 
        {
            ResponseCode = 0,
            ResponseMessage = "Failure",
            Data = errors
        };

        // Return a clean 400 response
        return new BadRequestObjectResult(new { response });
    };
});


//Build the web application
var app = builder.Build();

// for custom middleware
app.UseExcepetionHandlingMiddleware();

// for Routing
app.UseRouting();

// for swagger
app.UseSwagger();
app.UseSwaggerUI();

// for cors
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();

app.Run();
