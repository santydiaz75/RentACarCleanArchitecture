using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using RentACarCleanArchitecture.Api.Controllers.Alquileres;
using RentACarCleanArchitecture.Api.Documentation;
using RentACarCleanArchitecture.Api.Extensions;
using RentACarCleanArchitecture.Api.OptionsSetup;
using RentACarCleanArchitecture.Application;
using RentACarCleanArchitecture.Application.Abstractions.Authentication;
using RentACarCleanArchitecture.Infrastructure;
using RentACarCleanArchitecture.Infrastructure.Authentication;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog( (context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration)
);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddAuthorization();
builder.Services
.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services
.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName?.Replace("+", ".") ?? type.Name);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Introduce solo el token JWT, sin el prefijo Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "RentACarCleanArchitecture.Api v1");
    });
}

await app.ApplyMigration();
app.SeedData();
app.SeedDataAuthentication();



app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ApiVersionSet apiVersion = app.NewApiVersionSet()
      .HasApiVersion(new ApiVersion(1))
      .ReportApiVersions()
      .Build();

var routeGroupBuilder = app
.MapGroup("api/v{version:apiVersion}")
.WithApiVersionSet(apiVersion);

routeGroupBuilder.MapAlquilerEndpoints();

app.Run();

