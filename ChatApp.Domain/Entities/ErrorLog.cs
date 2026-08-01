namespace ChatApp.Domain.Entities;

public class ErrorLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? TargetSite { get; set; }
    public string? InnerException { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
