using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicyDev", builder =>
    {   
                builder.WithOrigins("http://localhost:3000", "http://localhost:3001")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
});

options.AddPolicy("CorsPolicyProd", builder =>
    {   
                builder.WithOrigins("http://ProductionURL.com")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
});
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicyDev");
}
else
{   
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicyProd");
}

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     // var forecast = Enumerable.Range(1, 5).Select(index =>
//     //     new WeatherForecast
//     //     (
//     //         DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//     //         Random.Shared.Next(-20, 55),
//     //         summaries[Random.Shared.Next(summaries.Length)]
//     //     ))
//     //     .ToArray();
//     // return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();


app.MapControllers();

app.Run();
