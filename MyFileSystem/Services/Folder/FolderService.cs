using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Entities;
using MyFileSystem.Persistence;
using MyFileSystem.Persistence.UnitOfWork;
using MyFileSystem.Services.Interfaces.File;
using MyFileSystem.Services.Interfaces.Folder;
using MyFileSystem.Validators;

namespace MyFileSystem.Services.Folder
{
    public class FolderService : IFolderService
    {
       
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager  _fileManager;
        //private IFoldersRepository;
        //private readonly FileSystemDbContext _context;
        //private readonly IWebHostEnvironment _web;

        public FolderService(IMapper mapper, IUnitOfWork unitOfWork, IFileManager fileManager /*IWebHostEnvironment web,*/)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
            //_web = web;
            //_context = context;
        }

        public async Task<FolderDto> CreateFolders(CreateFolderDto cFolderDto)
        {
            FolderValidator validator = new FolderValidator();
            if (!validator.Validate(cFolderDto).IsValid) throw new Exception("Not Valid... ");
            var path = _fileManager.GetRootPath() + cFolderDto.FolderName;
            if (cFolderDto.FolderParentId == null)
            {
                _fileManager.CreateDirectory(path);

                var folder = _mapper.Map<CreateFolderDto, Entities.Folder>(cFolderDto);
                folder.FolderPath = path;
                _unitOfWork.FoldersRepository.Add(folder);
                //await _context.Folders.AddAsync(folder);
                await _unitOfWork.CompleteAsync();
                var x = _mapper.Map<Entities.Folder, FolderDto>(folder);
                return x;
            }
            else 
            {
                var pFolder = await _unitOfWork.FoldersRepository.GetById(cFolderDto.FolderParentId);                       //_context.Folders.FindAsync(cFolderDto.FolderParentId); 
                path = pFolder.FolderPath + "\\" + cFolderDto.FolderName; 
               
                //Directory.CreateDirectory(path);
                _fileManager.CreateDirectory(path);

                var folder = _mapper.Map<CreateFolderDto, Entities.Folder>(cFolderDto);
                
                folder.FolderPath = path;
                _unitOfWork.FoldersRepository.Add(folder);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<Entities.Folder, FolderDto>(folder);
            }
        }
        public async Task<FolderDto> GetFolder(int id)
        {
            var folder = (await _unitOfWork.FoldersRepository
                .GetAllIncluded(f => f.FolderId == id, o => o.Files/*, o => o.FolderParent*/)).SingleOrDefault();              //.Folders.Include(f => f.Files).SingleOrDefaultAsync(f => f.FolderId == id);
            var folders = await _unitOfWork.FoldersRepository.GetAll(f => f.FolderParentId == id);

            if (folder == null)
                throw new Exception("Not Found... ");                                                                          // var folder2 = await _unitOfWork.FoldersRepository.GetAll(f=>f.FolderId == id, "Files,FolderParent");  

            var folderDto = _mapper.Map<Entities.Folder, FolderDto>(folder);
            folderDto.Folders = _mapper.Map<List<Entities.Folder>, List<FolderDto>>(folders.ToList());
            return folderDto;
        }
        public async Task<List<FolderParentDto>> GetFolders()
        {
            var folder = await _unitOfWork.FoldersRepository.GetAllIncluded(f => f.FolderId != null, o => o.Files);                                          //_context.Folders.Include(m => m.Files).Include(e => e.FolderParent).ToListAsync();
            var folders = await _unitOfWork.FoldersRepository.GetAll();      // _context.Folders.ToListAsync();

            if (folder == null)
                throw new Exception("Not Found... ");

            return _mapper.Map<List<Entities.Folder>, List<FolderParentDto>>(folders.ToList());
        }
        public async Task<string> UpdateFolders(int id, [FromForm] string path2)
        {
            var folder = (await _unitOfWork.FoldersRepository.GetAllIncluded(f => f.FolderId == id, o => o.Files)).SingleOrDefault(); ;//_context.Folders.Include(f => f.Files).SingleOrDefaultAsync(f => f.FolderId == id);
            if (folder == null)
                throw new Exception("Not Found... ");
            try
            {
                if (!Directory.Exists(path2))
                {
                    Directory.Move(folder.FolderPath, folder.FolderPath + "\\" + path2);

                    await _unitOfWork.CompleteAsync();
                    _mapper.Map<Entities.Folder, CreateFolderDto>(folder);
                    return ("Directory was Moved... ");
                }
                else throw new Exception("Not Found... "); ;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public async Task<string> DeleteFolders(int id)
        {
            var folder = await _unitOfWork.FoldersRepository.GetById(id);                                 //_context.Folders.FindAsync(id);
            
            if (folder == null)
                throw new Exception("Not Found... ");

            await DeleteTree(id);
            _unitOfWork.FoldersRepository.Delete(folder);                                                 //_context.Folders.Remove(folder);
            var path = folder.FolderPath;
            //Directory.Delete(path, true);
            _fileManager.DeleteDirectory(path);
            await _unitOfWork.CompleteAsync();
            return ("Folder and it's contents were deleted... ");
        }
        public async Task DeleteTree(int fId)
        {
            var folders = (await _unitOfWork.FoldersRepository.GetAllIncluded(f => f.FolderParentId == fId)).ToList();//_context.Folders.Where(f => f.FolderParentId == fId).ToList();
            var firstLevelFiles = (await _unitOfWork.FileRepository.GetAllIncluded(fi => fi.FolderId == fId)).ToList();// _context.Files.Where(fi => fi.FolderId == fId).ToList();
            if (firstLevelFiles.Count > 0)
            {  await _unitOfWork.FileRepository.DeleteRange(firstLevelFiles); }//_context.Files.RemoveRange(firstLevelFiles); }

            foreach (var folder in folders)
            {
                await DeleteTree(folder.FolderId);
                var files = (await _unitOfWork.FileRepository.GetAllIncluded(fi => fi.FolderId == fId)).ToList();//_context.Files.Where(fi => fi.FolderId == fId).ToList();
                if (files.Count > 0)
                {
                 await  _unitOfWork.FileRepository.DeleteRange(files);
                   //_context.Files.RemoveRange(files);
                }
               _unitOfWork.FoldersRepository.Delete(folder);//_context.Folders.Remove(folder);
            }
        }
    }
}
