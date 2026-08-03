namespace ChatApp.Api.Configuration;

public class JwtConfiguration
{
    public string Issuer { get; set; } = string.Empty;
    public string CLient { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int JwtExpirationMinutes {get;set;}

}
