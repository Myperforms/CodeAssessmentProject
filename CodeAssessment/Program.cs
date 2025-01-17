using Application;
using CodeAssessment.Infrastructure;
using CodeAssessment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Code Assessment API", Version = "v1" });   
});

builder.Services.AddApplicationServices();

DependencyContainer.RegisterServices(builder.Services);
ApplicationServiceRegistration.RegisterServices(builder.Services);

builder.Services.AddDbContext<CodeAssessmentDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodeAssessmentConnection")).EnableSensitiveDataLogging();
});


builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddFile("Logs/CodeAssessment-{Date}.txt");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeAssessment v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
