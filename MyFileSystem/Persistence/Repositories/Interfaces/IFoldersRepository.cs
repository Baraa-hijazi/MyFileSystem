using System.Threading.Tasks;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Entities;
using MyFileSystem.Persistence.Repositories;

namespace MyFileSystem.Persistence 
{
    public interface IFoldersRepository : IBaseRepository<Folder>
    {
        //Task<FolderDto> GetFolder(int id);
    }
}