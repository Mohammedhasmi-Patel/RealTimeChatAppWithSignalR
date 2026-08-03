using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.DTO.Auth.Response;
using ChatApp.Application.DTO.Common;
using ChatApp.Application.Exceptions;
using ChatApp.Application.ServiceContracts.Auth;
using ChatApp.Domain.Enum;
using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;


    public AuthService(UserManager<ApplicationUser> userManager,ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<ApiResponse<LoginUserResponse>> LoginUserAsync(LoginUserRequest request)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException("User not found.");

        bool isPasswordMtach = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordMtach)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        string token = _tokenService.GenerateJwtToken(user);    

    }


    public async Task<ApiResponse<object>> RegisterUserAsync(RegisterUserRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is not null)
        {
            throw new ConflictException("User already exist");
        }

        var newUser = new ApplicationUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = await GenerateUniqueUserNameAsync(request.FirstName, request.LastName),
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            throw new BadRequestException(result.Errors.First().Description);
        }

        var roleResult = await _userManager.AddToRoleAsync(newUser, nameof(UserRoleEnum.User));
        if (!roleResult.Succeeded)
        {
            throw new BadRequestException(roleResult.Errors.First().Description);
        }
        return ApiResponse<object>.SuccessResponse(null,StatusCodes.Status201Created,"User created successfully.");

    }

    private async Task<string> GenerateUniqueUserNameAsync(string firstName, string lastName)
    {
        var baseUserName = $"{firstName.Trim().ToLower()}.{lastName.Trim().ToLower()}";
        var userName = baseUserName;
        var count = 1;

        while (await _userManager.FindByNameAsync(userName) != null)
        {
            userName = $"{baseUserName}{count++}";
        }

        return userName;
    }

}
