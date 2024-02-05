using APIWeb.Auth;
using APIWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{

    x.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Description = "Api Key to Secure the Api",
        Type = SecuritySchemeType.ApiKey,
        Name = AuthConfig.ApiKeyHeader,
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"


    });

    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference()
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey",
        },
        In = ParameterLocation.Header


    };
    var requirement = new OpenApiSecurityRequirement()
    {
        {scheme,new List<string>()}
    };

    x.AddSecurityRequirement(requirement);

});

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlite(@"Data Source=Database.sqlite");
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
