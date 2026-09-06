

using TodoWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITodoService, TodoService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

// Read from app settings
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();


if (allowedOrigins != null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AngularClient", policy =>
        {
            policy
                .WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}


var app = builder.Build();
if (allowedOrigins != null)
{
    app.UseCors("AngularClient");
}
// swagger UI for dev / testing purposes
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "Todo API v1");
    });
}

app.MapControllers();

app.Run();
