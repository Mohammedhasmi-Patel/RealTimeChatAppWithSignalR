using ChatApp.Api.Extension;
using ChatApp.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAllServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
