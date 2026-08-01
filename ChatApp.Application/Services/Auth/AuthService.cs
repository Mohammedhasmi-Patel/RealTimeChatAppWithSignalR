using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.Exceptions;
using ChatApp.Application.RepositoryContracts.Auth;
using ChatApp.Application.ServiceContracts.Auth;

namespace ChatApp.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public async Task<object> RegisterUserAsync(RegisterUserRequest request)
    {
        throw new NotImplementedException();
    }

}
