﻿using MyFileSystem.Core.Entities;
using MyFileSystem.Entities;
using MyFileSystem.Persistence.Repositories;
using System.Threading.Tasks;

namespace MyFileSystem.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<File> FileRepository { get; }
        IBaseRepository<Folder> FoldersRepository { get; }
        IBaseRepository<ApplicationUser> AccountRepository { get; }
        Task CompleteAsync();
    }
}
