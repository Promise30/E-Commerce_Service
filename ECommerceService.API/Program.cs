using ECommerceService.API.Application.Implementation;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data;
using ECommerceService.API.Extensions;
using ECommerceService.API.Helpers;
using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
    loggingBuilder.AddSerilog();
});
builder.Services.AddRouting(opt =>
{
    opt.LowercaseUrls = true;
    opt.LowercaseQueryStrings = true;
});


// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Add configuration based on environment
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configure Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure Database
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepository();

// Services
builder.Services.ConfigureHostingServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// Add hangfire services
builder.Services.ConfigureHangfire(builder.Configuration);

// Add email service
builder.Services.AddFluentEmail(builder.Configuration);
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Configure Identity
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthentication(builder.Configuration);

// Configure HealthChecks
builder.Services.ConfigureHealthChecks(builder.Configuration);
//builder.Services.AddHealthChecksUI().AddInMemoryStorage();

// configure paystack
builder.Services.AddHttpClient("PayStackClient", options =>
{
    options.BaseAddress = builder.Configuration.GetSection("PayStack:BaseUrl").Get<Uri>();
    options.DefaultRequestHeaders.Add("Authorization", builder.Configuration["PayStack:SecretKey"]);
    options.DefaultRequestHeaders.Add("Content-Type", "application/json");

});

builder.Services.AddScoped<PayStackApi>(ps =>
    new PayStackApi(builder.Configuration["PayStack:SecretKey"]));

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await RoleSeeder.SeedRole(serviceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply migrations to database
using (var service = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = service.ServiceProvider.GetRequiredService<ECommerceDbContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/health/custom", new HealthCheckOptions()
{
    Predicate = (check) => check.Tags.Contains("custom"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("Application has started");
    logger.LogInformation("App is Running : " + builder.Environment.EnvironmentName);
});

GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

app.Run();
