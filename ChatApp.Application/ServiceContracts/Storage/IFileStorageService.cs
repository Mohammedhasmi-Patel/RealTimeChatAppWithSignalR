using ChatApp.Application.DTO.FileStorage;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Application.ServiceContracts.Storage;

public interface IFileStorageService
{
    Task<FileStorageResponse> UploadAsync(IFormFile file, FileUploadRequest request, CancellationToken cancellationToken = default);
    Task<List<FileStorageResponse>> UploadManyAsync(IEnumerable<IFormFile> files, FileUploadRequest request, CancellationToken cancellationToken = default);
    Task<FileStorageResponse> UpdateAsync(IFormFile file,FileUploadRequest request,string? oldFilePath,CancellationToken cancellationToken = default);

    bool DeleteFile(string filePath);
    bool ExistsTheFile(string filePath);
    string GetFileUrl(string filePath);
}
