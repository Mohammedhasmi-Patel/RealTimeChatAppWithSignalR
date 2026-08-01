namespace ChatApp.Application.DTO.FileStorage;

public class FileStorageResponse
{
    public string FileName { get; set; } = default!;

    public string OriginalFileName { get; set; } = default!;

    public string FilePath { get; set; } = default!;

    public string Url { get; set; } = default!;

    public long SizeInKbs { get; set; }
    public string Extension { get; set; } = default!;
}
