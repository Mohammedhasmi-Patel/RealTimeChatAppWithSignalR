using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.RepositoryContracts.Auth;
using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public Task AddUserAsync(RegisterUserRequest request, string passwordHash)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        ApplicationUser? res = await _userManager.FindByEmailAsync(email);
        return res != null;
    }

}
