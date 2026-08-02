using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.DTO.Auth.Response;
using ChatApp.Application.DTO.Common;
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

    public Task<ApiResponse<LoginUserResponse>> LoginUserAsync(LoginUserRequest request)
    {
        throw new NotImplementedException();
    }


    public async Task<object> RegisterUserAsync(RegisterUserRequest request)
    {
        AddUserResponse result = await _authRepository.AddUserAsync(request);

        if (!result.Success)
        {
            string message = result.Message;
            throw new BadRequestException(message);
        }
        return ApiResponse<object>.SuccessResponse(null,201,"User created successfully.");
    }

}
