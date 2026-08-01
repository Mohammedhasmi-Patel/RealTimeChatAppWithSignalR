using ChatApp.Application.DTO.Auth.Requests;

namespace ChatApp.Application.RepositoryContracts.Auth;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task AddUserAsync(RegisterUserRequest request, string passwordHash);

}
