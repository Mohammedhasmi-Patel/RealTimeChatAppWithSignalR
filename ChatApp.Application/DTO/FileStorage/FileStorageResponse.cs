namespace ChatApp.Application.DTO.FileStorage;

public class FileStorageResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = "File upload failed.";

    public string? FileName { get; set; }

    public string? OriginalFileName { get; set; }

    public string? FilePath { get; set; }

    public string? Url { get; set; }

    public long? SizeInKbs { get; set; }
    public string? Extension { get; set; }
}
