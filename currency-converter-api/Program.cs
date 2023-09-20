using System;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register HttpClient for dependency injection
builder.Services.AddHttpClient<CurrencyService>(client =>
{
    client.BaseAddress = new Uri("https://www.ecb.europa.eu/");
    client.DefaultRequestHeaders.Add("Accept", "application/xml");
});

// Swagger configurations
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();