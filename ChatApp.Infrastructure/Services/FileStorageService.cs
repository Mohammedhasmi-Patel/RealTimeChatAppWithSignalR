using ChatApp.Application.DTO.FileStorage;
using ChatApp.Application.ServiceContracts.Storage;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Infrastructure.Services;

public class FileService : IFileStorageService
{
    public Task<bool> DeleteAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public string GetFileUrl(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<FileStorageResponse> UpdateAsync(IFormFile file, FileUploadRequest request, string? oldFilePath, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FileStorageResponse> UploadAsync(IFormFile file, FileUploadRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<FileStorageResponse>> UploadManyAsync(IEnumerable<IFormFile> files, FileUploadRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}
