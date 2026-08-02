namespace ChatApp.Application.DTO.Auth.Requests;

public class LoginUserRequest
{
    public string Email { get; set; } = null!;
    public string Password {get;set;} = null!;
}
