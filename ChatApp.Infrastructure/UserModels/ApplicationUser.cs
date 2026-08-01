using Microsoft.AspNetCore.Identity;

namespace ChatApp.Infrastructure.UserModels;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string StatusMessage { get; set; } = string.Empty;

    public DateTime? LastSeenAt { get; set; }
    public bool IsOnline { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
