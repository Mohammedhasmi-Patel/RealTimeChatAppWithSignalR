using ChatApp.Application.DTO.FileStorage;
using ChatApp.Application.ServiceContracts.Storage;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{

    public async Task<FileStorageResponse> UploadAsync(IFormFile file, FileUploadRequest request, CancellationToken cancellationToken = default)
    {
        // throw new NotImplementedException();

        var result = new FileStorageResponse()
        {
            Success = false
        };

        // file validation
        if (file == null || file.Length == 0)
        {
            result.Message = "File is required";
            return result;
        }

        // size 
        if (file.Length > request.MaxFileSize)
        {
            result.Message = $"Maximum file size is {request.MaxFileSize}";
            return result;
        }

        // extension 
        List<string> allowedExtensions = request.AllowedExtensions;
        string currentFileExtension = Path.GetExtension(file.FileName);
        if (!allowedExtensions.Any(x => x.Equals(currentFileExtension)))
        {
            result.Message = $"Only {string.Join(",", allowedExtensions)} are allowed.";
            return result;
        }


        string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", request.RootPath);
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string fileName = $"{Guid.CreateVersion7()}{currentFileExtension}";
        string filePath = Path.Combine(uploadFolder, fileName);

        await using (var stream = new FileStream(filePath,FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

            result.Success = true;
            result.FileName = fileName;
            result.OriginalFileName = file.FileName;
            result.Extension = currentFileExtension;
            result.FilePath = filePath.Replace("\\", "/");
            result.SizeInKbs = (long) Math.Round(file.Length / 1024d, 2);
            result.Url = result.FilePath;
            result.Message = "File uploaded successfully.";
            return result;
    }

    public Task<List<FileStorageResponse>> UploadManyAsync(IEnumerable<IFormFile> files, FileUploadRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FileStorageResponse> UpdateAsync(IFormFile file, FileUploadRequest request, string? oldFilePath, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public bool DeleteFile(string filePath)
    {
        throw new NotImplementedException();
    }

    public bool ExistsTheFile(string filePath)
    {
        throw new NotImplementedException();
    }

    public string GetFileUrl(string filePath)
    {
        throw new NotImplementedException();
    }

}
