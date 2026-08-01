using ChatApp.Application.DTO.Auth.Requests;

namespace ChatApp.Application.ServiceContracts.Auth;

public interface IAuthService
{
    public Task<object> RegisterUserAsync(RegisterUserRequest request);
}
