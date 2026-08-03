using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.DTO.Auth.Response;
using ChatApp.Application.DTO.Common;

namespace ChatApp.Application.ServiceContracts.Auth;

public interface IAuthService
{
    public Task<ApiResponse<object>> RegisterUserAsync(RegisterUserRequest request);
    public Task<ApiResponse<LoginUserResponse>> LoginUserAsync(LoginUserRequest request);

}
