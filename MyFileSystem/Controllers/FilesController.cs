using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Persistence;
using System.Threading.Tasks;
using MyFileSystem.Services.Interfaces.File;
using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

namespace MyFileSystem.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {_fileService = fileService;}

        [HttpGet]
        public async Task<IActionResult> GetFiles() => Ok(await _fileService.GetFiles());
        //{
        //    var file = await _context.Files.ToListAsync();
        //    var fileDto = _mapper.Map<List<Entities.File>, List<FileDto>>(file);
        //    if (file == null)
        //        return NotFound();
        //    return Ok(fileDto);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFiles(int id) => Ok(await _fileService.GetFiles(id));
        //{
        //    var file = await _context.Files.FindAsync(id);
        //    if (file == null)
        //        return NotFound();

        //    var fileDto = _mapper.Map<Entities.File, FileDto>(file);
        //    return Ok(fileDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFiles(int id, [FromBody] CreateFileDto createFileDto) => Ok(await _fileService.UpdateFiles(id, createFileDto));
        //{
        //    var file = await _context.Files.FindAsync(id);
        //    if (file == null)
        //        return NotFound();
        //    file = _mapper.Map(createFileDto, file);
        //    await _context.SaveChangesAsync();
        //    _mapper.Map<Entities.File, CreateFileDto>(file);

        //    return Ok("File was updated... ");
        //}
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiles(int id) => Ok(await _fileService.DeleteFiles(id));
        //{
        //    var file = await _context.Files.FindAsync(id);
        //    if (file == null)
        //        return BadRequest("File does not exist! ");

        //    var path = Path.GetFullPath(file.FilePath);                   //Directory.GetCurrentDirectory() + "\\" + "wwwroot" + "\\" + file.FileName;
        //    System.IO.File.Delete(path);

        //    _context.Remove(file);
        //    await _context.SaveChangesAsync();

        //    return Ok("File was deleted Physically and From The Database... ");
        //}

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] int folderId) => Ok(await _fileService.UploadFile(file, folderId));
        //{
        //    try
        //    {
        //        //--------------- Upload Physical File  ----------------//
        //        var path = "";
        //        if (file == null)
        //            return Content("File not selected");
        //        if (folderId > 0)
        //        {
        //            var rootFolder = await _context.Folders.Where(f => f.FolderId == folderId).SingleOrDefaultAsync();
        //            if (rootFolder == null) { return Content("Folder not found"); }
        //            else { path = rootFolder.FolderPath + '\\' + file.FileName; }
        //        }
        //        else
        //        { path = Directory.GetCurrentDirectory() + "\\" + "wwwroot" + "\\" + file.FileName; }
        //        await using (var stream = System.IO.File.Create(path))          //new FileStream(path, FileMode.Open))
        //        {
        //            await file.CopyToAsync(stream);
        //            stream.Flush();
        //        }
        //        //----------------------- Save to Db ---------------------//
        //        var entityFile = new Entities.File
        //        {
        //            FileName = Path.GetFileNameWithoutExtension(path),          //(path).Split(".").FirstOrDefault(),//FileName = file.FileName.Split(".").FirstOrDefault(),
        //            FileExtension = Path.GetExtension(file.FileName),           //FileExtension = file.FileName.Split(".").LastOrDefault(),
        //            FileSize = int.Parse(file.Length.ToString()),               //FileSize = int.Parse(file.Length.ToString()),
        //            FilePath = Path.GetFullPath(path)                           //FilePath = path
        //        };
        //        var f = new FileInfo(Path.GetFullPath(path));
        //        if (!System.IO.File.Exists(Directory.GetCurrentDirectory() + file.FileName))//f.Exists)  
        //        {
        //            if (folderId > 0)
        //            { entityFile.FolderId = folderId; }
        //            _context.Add(entityFile);
        //            await _context.SaveChangesAsync();
        //            var result = _mapper.Map<Entities.File, CreateFileDto>(entityFile);
        //            return Ok(result);
        //        }
        //        else { return BadRequest("File Already exists... "); }
        //    }
        //    catch (Exception e) { return BadRequest(e.Message); }
        //}
    }
}


 //file.Exists(entityFile.FileName))//entityFile.FilePath))  //(entityFile.FilePath != Path.GetFullPath(path))   //(Directory.Exists(entityFile.FilePath))//(entityFile.FilePath != null)
 //(!entityFile.FileName.Equals(file.FileName))
//System.IO.File.Exists(Path.GetFullPath(path)))    
//{entityFile.FileName = Path.GetFileNameWithoutExtension(path) + "(1)";}






//[HttpPost("x")]
//public async Task<IActionResult> UploadFile(IFormFile file)
//{
//    try
//    {
//        if (file == null)//|| file.Length == 0)
//            return Content("file not selected");

//        var path = Directory.GetCurrentDirectory() + "\\" + "wwwroot" + "\\" + file.FileName;
//        await using (var stream = System.IO.File.Create(path))//new FileStream(path, FileMode.Open))
//        {
//            await file.CopyToAsync(stream);
//            stream.Flush();
//        }

//        var file1 = new Entities.File
//        {
//            FileName = file.FileName.Split(".").FirstOrDefault(),
//            FileExtension = file.FileName.Split(".").LastOrDefault(),
//            FileSize = int.Parse(file.Length.ToString()),
//            FilePath = path,
//        };

//        if (!Directory.Exists(file1.FilePath))
//        {
//            _context.Add(file1);
//            await _context.SaveChangesAsync();
//            var result = _mapper.Map<Entities.File, CreateFileDto>(file1);
//            return Ok(result);
//        }
//        else { return Ok("File Already exists... "); }
//    }
//    catch (Exception e) { return BadRequest(e.Message); }
//}







//[HttpPost]
//public async Task<IActionResult> CreateFiles([FromBody] CreateFileDto createFileDto)
//{
//    var file = _mapper.Map<CreateFileDto, Entities.File>(createFileDto);
//    _context.Add(file);
//    await _context.SaveChangesAsync();
//    var result = _mapper.Map<Entities.File, CreateFileDto>(file);
//    return Ok(result);
//}









//try
//{
//if (file == null || file.Length == 0)
//return /*Content*/("file not selected");

//var path = Directory.GetCurrentDirectory() + "//" + "wwwroot" + "//" + file.FileName;
//await using (FileStream stream = System.IO.File.Create(path))//new FileStream(path, FileMode.Open))
//{
//    await file.CopyToAsync(stream);
//    stream.Flush();
//    return file.FileName;
//}

//var file1 = new Entities.File
//{
//    //FileId = file. 
//    FileName = file.FileName.Split(".").FirstOrDefault(),
//    FileExtension = file.FileName.Split(".").LastOrDefault(),
//    FileSize = int.Parse(file.Length.ToString())
//    //FolderId =
//};
//_context.Add(file1);
////await _context.SaveChangesAsync();
//var result = _mapper.Map<Entities.File, CreateFileDto>(file1);
//return Ok(result);
////return Ok(file1);
//}
//catch (Exception e)
//{
//return e.Message.ToString();
//}







//public async Task<IActionResult> Download(string filename)
//{
//    if (filename == null)
//        return Content("filename not present");

//    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
//    var memory = new MemoryStream();
//    await using (var stream = new FileStream(path, FileMode.Create)) { await stream.CopyToAsync(memory); }
//    memory.Position = 0;
//    return File(memory, GetContentType(path), Path.GetFileName(path));
//}
//private string GetContentType(string path)
//{
//    var types = GetMimeTypes();
//    var ext = Path.GetExtension(path).ToLowerInvariant();
//    return types[ext];
//}
//private Dictionary<string, string> GetMimeTypes()
//{
//    return new Dictionary<string, string>
//    {
//        {".txt", "text/plain"},
//        {".pdf", "application/pdf"},
//        {".doc", "application/vnd.ms-word"},
//        {".docx", "application/vnd.ms-word"},
//        {".xls", "application/vnd.ms-excel"},
//        {".xlsx", "application/vnd.openxmlformats" +
//                  "officedocument.spreadsheetml.sheet"},
//        {".png", "image/png"},
//        {".jpg", "image/jpeg"},
//        {".jpeg", "image/jpeg"},
//        {".gif", "image/gif"},
//        {".csv", "text/csv"}
//    };
//}