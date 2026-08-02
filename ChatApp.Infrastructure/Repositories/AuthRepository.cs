using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.DTO.Auth.Response;
using ChatApp.Application.Helpers;
using ChatApp.Application.RepositoryContracts.Auth;
using ChatApp.Domain.Enum;
using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AddUserResponse> AddUserAsync(RegisterUserRequest request)
    {
        string avatarUrl = string.Empty;

        if (await EmailExistsAsync(request.Email))
        {
            return new AddUserResponse
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Avatar = avatarUrl,
            Email = request.Email,
            UserName = await GenerateUserName(request.FirstName, request.LastName),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return new AddUserResponse
            {
                Success = false,
                Message = string.Join("; ", createResult.Errors.Select(error => error.Description))
            };
        }

        var roleResult = await _userManager.AddToRoleAsync(user, nameof(UserRoleEnum.User));
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return new AddUserResponse
            {
                Success = false,
                Message = string.Join("; ", roleResult.Errors.Select(error => error.Description))
            };
        }

        return new AddUserResponse
        {
            Success = true,
            Message = "User created successfully."
        };
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        return existingUser is not null;
    }

    public async Task<LoginUserResponse> LoginUserAsync(LoginUserRequest request)
    {
        
    }


    private async Task<string> GenerateUserName(string firstName, string lastName)
    {
        var baseUserName = $"{firstName.Trim().ToLower()}.{lastName.Trim().ToLower()}";
        var userName = baseUserName;
        var counter = 1;

        while (await _userManager.Users.AnyAsync(x => x.UserName == userName))
        {
            userName = $"{baseUserName}{counter++}";
        }

        return userName;
    }
}
