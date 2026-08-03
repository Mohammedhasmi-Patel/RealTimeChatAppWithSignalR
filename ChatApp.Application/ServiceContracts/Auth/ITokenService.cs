using ChatApp.Infrastructure.UserModels;

namespace ChatApp.Application.ServiceContracts.Auth;

public interface ITokenService
{
    public string GenerateJwtToken(ApplicationUser applicationUser);
}
