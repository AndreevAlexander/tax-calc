using System.Text.Json.Serialization;
using TaxCalculator.WebApi.Extensions;

const string CorsPolicy = "local";

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCqrs();
builder.Services.AddValidation();
builder.Services.AddCors(x =>
{
    x.AddPolicy(CorsPolicy, policy => policy.WithOrigins("https://localhost:7015").WithHeaders("*"));
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();