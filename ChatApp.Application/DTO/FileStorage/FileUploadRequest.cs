namespace ChatApp.Application.DTO.FileStorage;

public class FileUploadRequest
{
    public string RootPath { get; set; } = "Uploads";

    public long MaxFileSize { get; set; } = 5 * 1024 * 1024;

    public List<string> AllowedExtensions { get; set; } = [];

}
