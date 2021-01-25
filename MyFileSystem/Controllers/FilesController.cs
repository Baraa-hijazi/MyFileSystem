﻿using Microsoft.AspNetCore.Mvc;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Services.Interfaces.File;
using System.Threading.Tasks;
using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

namespace MyFileSystem.Controllers
{
    [Route("Files/[Controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> UploadFile([FromForm] CreateFileDto createFileDto) => Ok(await _fileService.UploadFile(createFileDto));

        //[HttpGet("Get-All")]
        //public async Task<IActionResult> GetFiles() => Ok(await _fileService.GetFiles());

        [HttpGet("Get-All-Paged")]
        public async Task<IActionResult> GetFiles(int? pageIndex, int? pageSize) => Ok(await _fileService.GetFiles(pageIndex, pageSize));

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetFile(int id) => Ok(await _fileService.GetFile(id));

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateFiles(int id, [FromBody] UpdateFileDto updateFileDto) => Ok(await _fileService.UpdateFiles(id, updateFileDto));

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteFiles(int id) => Ok(await _fileService.DeleteFiles(id));
    }
}