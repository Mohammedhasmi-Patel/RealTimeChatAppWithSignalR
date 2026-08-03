using ChatApp.Api.Configuration;
using ChatApp.Application.DTO.Auth.Validators;
using ChatApp.Application.DTO.Common;
using ChatApp.Application.DTO.FileStorage;
using ChatApp.Application.ServiceContracts.Auth;
using ChatApp.Application.Services.Auth;
using ChatApp.Infrastructure.Database;
using ChatApp.Infrastructure.UserModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Extension;
public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddHttpContextAccessor();
        service.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        string firstMessage = context.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                                .Select(e => e.Value!.Errors.FirstOrDefault()?.ErrorMessage)
                                                                .FirstOrDefault() ?? "Validation Error";

                        return new BadRequestObjectResult(ApiResponse<object>.FailureResponse(422,firstMessage));

                    };
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserRequestValidator>());

        service.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
        service.AddTransient<ITokenService,TokenService>();

        #region Swagger Stuff
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        #endregion
        #region Database Stuff

        #region file storage stuff
        service.Configure<FileUploadRequest>(configuration.GetSection("FileStorage"));

        #endregion
        string databaseUrl = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Dburl not found");
        service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseUrl));
        service.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        #endregion

        service.AddScoped<IAuthService, AuthService>();

        return service;
    }
}
