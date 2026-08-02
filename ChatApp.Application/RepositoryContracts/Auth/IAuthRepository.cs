using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.DTO.Auth.Response;

namespace ChatApp.Application.RepositoryContracts.Auth;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task<AddUserResponse> AddUserAsync(RegisterUserRequest request);

}
