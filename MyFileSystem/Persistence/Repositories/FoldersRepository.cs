using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Entities;
using MyFileSystem.Persistence.Repositories;

namespace MyFileSystem.Persistence 
{
    public class FoldersRepository : BaseRepository<Folder> , IFoldersRepository
    {
        private readonly FileSystemDbContext _context;
        private readonly IMapper _mapper;

        public FoldersRepository(FileSystemDbContext context ,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<FolderDto> GetFolder(int id)
        //{
        //    //Folder folders = await _context.Folders.FindAsync(id);
        //    //return _mapper.Map<Folder, FolderDto>(folders);

        //    var folder = await _context.Folders.Include(f => f.Files).SingleOrDefaultAsync(f => f.FolderId == id);
        //    var folders = await _context.Folders.Where(f => f.FolderParentId == id).ToListAsync();
        //    if (folder == null)
        //        throw new Exception("Not Found... ");

        //    var folderDto = _mapper.Map<Entities.Folder, FolderDto>(folder);
        //    folderDto.Folders = _mapper.Map<List<Entities.Folder>, List<FolderDto>>(folders);
        //    return folderDto;
        //}
    }
}
