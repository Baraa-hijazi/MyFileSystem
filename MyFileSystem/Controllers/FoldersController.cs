using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Services.Interfaces.Folder;
using System.Threading.Tasks;

namespace MyFileSystem.Controllers
{
    [Route("api/folders")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        { 
            _folderService = folderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFolders([FromBody] CreateFolderDto cFolderDto) => Ok(await _folderService.CreateFolders(cFolderDto));
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolders(int id) => Ok(await _folderService.GetFolder(id));

        [HttpGet]
        public async Task<IActionResult> GetFolders() => Ok(await _folderService.GetFolders());
        //{
        //    var folder = await _context.Folders.Include(m => m.Files)
        //        .Include(e => e.FolderParent).ToListAsync();
        //    var folders = await _context.Folders.ToListAsync();
        //    var folderDto = _mapper.Map<List<Folder>, List<FolderParentDto>>(folders);
        //    if (folder == null)
        //        return NotFound();
        //    return Ok(folderDto);
        //}

        //[HttpPut("{x}")]
        //public async Task<IActionResult> UpdateFolders(int id, [FromBody] CreateFolderDto cFolderDto)
        //{
        //    var folder = await _context.Folders.Include(f => f.Files).SingleOrDefaultAsync(f => f.FolderId == id);
        //    if (folder == null)
        //        return NotFound();
        //    folder = _mapper.Map(cFolderDto, folder);
        //    await _context.SaveChangesAsync();
        //    _mapper.Map<Folder, CreateFolderDto>(folder);
        //    return Ok("Folder was updated... ");
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFolders(int id, [FromForm] string path2) => Ok(await _folderService.UpdateFolders(id, path2));
        //{
        //    var folder = await _context.Folders.Include(f => f.Files).SingleOrDefaultAsync(f => f.FolderId == id);
        //    if (folder == null)
        //        return NotFound();
        //    //var path = folder.FolderPath;
        //    try
        //    {
        //        if (!Directory.Exists(path2))
        //        {
        //            //Directory.GetCurrentDirectory();
        //            Directory.Move(folder.FolderPath, folder.FolderPath + "\\" + path2);

        //            await _context.SaveChangesAsync();
        //            _mapper.Map<Folder, CreateFolderDto>(folder);
        //            return Ok("Directory was Moved... ");
        //        }
        //        else return BadRequest("Not Found... ");
        //    }
        //    catch (Exception e) { return BadRequest(e.Message); }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolders(int id) => Ok(await _folderService.DeleteFolders(id));
        //{
        //    var folder = await _context.Folders.FindAsync(id); 
        //    if (folder == null) 
        //         return NotFound();                                                     
        //    DeleteTree(id);
        //    _context.Folders.Remove(folder);
        //    var path = folder.FolderPath;
        //    Directory.Delete(path, true);
        //    await _context.SaveChangesAsync();
        //    return Ok("Folder and it's contents were deleted... ");
        //}

        //public void DeleteTree(int fId)
        //{
        //    var folders = _context.Folders.Where(f => f.FolderParentId == fId).ToList();
        //    var firstLevelFiles = _context.Files.Where(fi => fi.FolderId == fId).ToList();
        //    if (firstLevelFiles.Count > 0)
        //    { _context.Files.RemoveRange(firstLevelFiles); }

        //    foreach (var folder in folders)
        //    {
        //        DeleteTree(folder.FolderId);
        //        var files = _context.Files.Where(fi => fi.FolderId == fId).ToList();
        //        if (files.Count > 0) 
        //        { _context.Files.RemoveRange(files); }
        //        _context.Folders.Remove(folder);
        //    }
        //}
    } 
}



//_context.SaveChanges();

                    //foreach (var file in files)
                    //{
                    //    var path = Path.GetFullPath(file.FilePath);
                    //    _context.Files.Remove(file);
                    //    System.IO.File.Delete(path);
                    //}


                //var path = folder.FolderPath;
                //var aee = Directory.GetFileSystemEntries(path);
                //foreach (var fi in aee)
                //{
                //   _context.RemoveRange(files);
                //   Directory.Delete(fi,true);
                //}




        //public void GetPath()
        //{
        //    var folder = _context.Folders.Include(m => m.FolderId)
        //        .Include(e => e.FolderPath).SingleOrDefaultAsync(); //ToListAsync();
        //}
        //private void ShowFilesIn(string dir)
        //{
        //    DirectoryInfo dirInfo = new DirectoryInfo(dir);
        //    foreach (FileInfo fileItem in dirInfo.GetFiles())
        //    {
        //    }
        //}
        //private void ShowDirectoriesIn(string dir)
        //{
        //    DirectoryInfo dirInfo = new DirectoryInfo(dir);
        //    foreach (DirectoryInfo dirItem in dirInfo.GetDirectories())
        //    {
        //    }
        //}