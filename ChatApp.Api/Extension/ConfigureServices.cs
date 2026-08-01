using ChatApp.Application.DTO.Auth.Validators;
using ChatApp.Infrastructure.Database;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Extension;
public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserRequestValidator>());
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        string databaseUrl = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Dburl not found");
        service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseUrl));
        return service;
    }
}
