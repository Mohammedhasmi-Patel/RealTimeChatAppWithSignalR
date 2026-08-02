using ChatApp.Application.DTO.Auth.Validators;
using ChatApp.Application.DTO.FileStorage;
using ChatApp.Application.ServiceContracts.Storage;
using ChatApp.Infrastructure.Database;
using ChatApp.Infrastructure.Services;
using ChatApp.Infrastructure.UserModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Extension;
public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserRequestValidator>());

        #region Swagger Stuff
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        #endregion
        #region Database Stuff

        #region file storage stuff
        service.Configure<FileUploadRequest>(configuration.GetSection("FileStorage"));
        // service.AddScoped<IFileStorageService, LocalFileStorageService>();

        #endregion
        string databaseUrl = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Dburl not found");
        service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseUrl));
        service.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        #endregion

        service.AddScoped<IFileStorageService,FileStorageService>();

        return service;
    }
}
