using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Orange_task.Domain.Entities;
using Orange_task.Domain.Interfaces;
using Orange_task.Domain.IService;
using Orange_task.Helper;
using Orange_task.Repository.Data;
using Orange_task.Repository.Repository;
using Orange_task.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Validate ConnectionString
var connectionString = builder.Configuration.GetConnectionString("CustomerConnection") ??
                    throw new InvalidOperationException("Error in Database Connection");

// Services liftime  => if Poject is large we can make Dependency class for every Class library
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .WithHeaders("http://localhost:4200/");
        });
});
var app = builder.Build();
// Update DB when it be changed
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var dbContext = services.GetRequiredService<CustomerDbContext>();
    await dbContext.Database.MigrateAsync();
    // Seed Data
    await CustomerDataSeeding.SeedAsync(dbContext);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occured during  apply the migration ");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
