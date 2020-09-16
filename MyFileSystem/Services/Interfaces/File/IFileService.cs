using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFileSystem.Core.DTOs;

namespace MyFileSystem.Services.Interfaces.File
{
    public interface IFileService
    {
        Task<List<FileDto>> GetFiles();
        Task<FileDto> GetFiles(int id);
        Task<FileDto> UploadFile(IFormFile file, int folderId);
        Task<string> UpdateFiles(int id, CreateFileDto createFileDto);
        Task<string> DeleteFiles(int id);
    }
}
