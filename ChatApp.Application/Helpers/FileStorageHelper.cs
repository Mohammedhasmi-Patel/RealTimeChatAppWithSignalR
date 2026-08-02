using ChatApp.Application.DTO.FileStorage;

namespace ChatApp.Application.Helpers;

public static class FileStorageHelper
{
    public static FileUploadRequest BuildFileUploadRequest(string folder, long maxFileSize, List<string> allowedExtensions)
    {
        return new FileUploadRequest
        {
            RootPath = folder,
            MaxFileSize = maxFileSize,
            AllowedExtensions = allowedExtensions
        };
    }

}
