using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Entities;
using MyFileSystem.Persistence;
using MyFileSystem.Persistence.Repositories;
using MyFileSystem.Persistence.UnitOfWork;
using MyFileSystem.Services.Interfaces.File;
using MyFileSystem.Validators;

namespace MyFileSystem.Services.File
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public FileService(IMapper mapper, IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<List<FileDto>> GetFiles()
        {
            //var files = await _context.Files.ToListAsync();
            var files = await _unitOfWork.FileRepository.GetAll();
            if (files == null)
                throw new Exception("Not Found... ");
            return _mapper.Map<List<Entities.File>, List<FileDto>>((List<Entities.File>)files); // ?? //
        }
        public async Task<FileDto> GetFiles(int id)
        {
            //var file = await _context.Files.FindAsync(id);
            var file = await _unitOfWork.FileRepository.GetById(id);
            if (file == null)
                throw new Exception("Not Found... ");
            //var stream = System.IO.File.OpenRead(Path.GetFullPath(Path.GetFullPath(file.FilePath)));
            //var reader = new StreamReader(stream);
            return _mapper.Map<Entities.File, FileDto>(file);
        }
        public async Task<FileDto> UploadFile( IFormFile file, int folderId)
        {
            FileValidator fileValidator = new FileValidator();
            if (!fileValidator.Validate(file).IsValid) throw new Exception("Empty_Null");

            //--------------- Upload Physical File ------------------//
            var path = "";
            if (file == null)
                throw new Exception("File not selected");
            if (folderId > 0)
            {
                var rootFolder = (await _unitOfWork.FoldersRepository.GetAllIncluded(f => f.FolderId == folderId)).SingleOrDefault();//_context.Folders.Where(f => f.FolderId == folderId).SingleOrDefaultAsync();
                if (rootFolder == null) { throw new Exception("Folder not found"); }
                path = rootFolder.FolderPath + '\\' + file.FileName; 
            }
            else
            {
                path = _fileManager.GetRootPath() + file.FileName;
            }
            _fileManager.UploadFile(file, path);
            //await using (var stream = System.IO.File.Create(path))
            //{
            //    await file.CopyToAsync(stream);
            //    stream.Flush();
            //}

            //----------------------- Save to Db ---------------------//
            var entityFile = new Entities.File
            {
                FileName = Path.GetFileNameWithoutExtension(path),
                FileExtension = Path.GetExtension(file.FileName),
                FileSize = int.Parse(file.Length.ToString()),
                FilePath = Path.GetFullPath(path),
                FolderId = folderId > 0 ? folderId : default(int?)
            };

            var f = new FileInfo(Path.GetFullPath(path));
            _unitOfWork.FileRepository.Add(entityFile);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Entities.File, FileDto>(entityFile);
        }
        public async Task<string> UpdateFiles(int id, [FromBody] CreateFileDto createFileDto)
        {
            var file = await _unitOfWork.FileRepository.GetById(id);
            if (file == null)
                throw new Exception("Not Found... ");
            
            file = _mapper.Map(createFileDto, file);
            await _unitOfWork.CompleteAsync();
            _mapper.Map<Entities.File, CreateFileDto>(file);
            return ("File was updated... ");
        }
        public async Task<string> DeleteFiles(int id)
        {
            var file = await _unitOfWork.FileRepository.GetById(id);
            if (file == null)
                throw new Exception("Not Found... ");

            var path = file.FilePath;
            
            //Path.GetFullPath(file.FilePath);

            _fileManager.DeleteFile(path);
            
            _unitOfWork.FileRepository.Delete(file);
            await _unitOfWork.CompleteAsync();

            return ("File was deleted... ");
        }
    }
}