using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.DTO.Auth.Response;
using ChatApp.Application.Helpers;
using ChatApp.Application.RepositoryContracts.Auth;
using ChatApp.Application.ServiceContracts.Storage;
using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFileStorageService _fileStorageService;

    public AuthRepository(UserManager<ApplicationUser> userManager, IFileStorageService fileStorageService)
    {
        _userManager = userManager;
        _fileStorageService = fileStorageService;
    }

    public async Task<AddUserResponse> AddUserAsync(RegisterUserRequest request)
    {
        string avatarUrl = string.Empty;

        if (request.Avatar is not null)
        {
            var uploadRequest = FileStorageHelper.BuildFileUploadRequest("avatar",2*1024*1024,["jpg","jpeg","webp"]);

            var fileUploadResult = await _fileStorageService.UploadAsync(request.Avatar, uploadRequest);

            if (!fileUploadResult.Success)
            {
                return new AddUserResponse()
                {
                    Success = false,
                    Message = "Something went wrong while adding user"
                };
            }

            avatarUrl = fileUploadResult.Url ?? fileUploadResult.FilePath ?? string.Empty;
        }

        var user = new ApplicationUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Avatar = avatarUrl,
            Email = request.Email,
            UserName = await GenerateUserName(request.FirstName, request.LastName),
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _userManager.CreateAsync(user, request.Password);

        return new AddUserResponse()
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
