namespace ChatApp.Application.DTO.Auth.Response;

public class LoginUserResponse
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;

    public string? Token { get; set; }

}
