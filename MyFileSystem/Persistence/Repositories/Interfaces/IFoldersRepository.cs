using MyFileSystem.Entities;
using MyFileSystem.Persistence.Interfaces;
using MyFileSystem.Persistence.Repositories;

namespace MyFileSystem.Persistence
{
    public interface IFoldersRepository : IBaseRepository<Folder>
    {
    }
}