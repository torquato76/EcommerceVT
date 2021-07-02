using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceVT.Services
{
    public class FileService : IFile
    {
        private readonly IList<File> _files = new List<File>();
        public static IHostEnvironment _environment;
        private readonly IHttpContextAccessor _context;

        public FileService(IHostEnvironment environment, IHttpContextAccessor context)
        {
            _environment = environment;
            _context = context;
        }

        public async Task<IList<File>> GetFiles(string folder, string subFolder)
        {
            var filePath = System.IO.Path.Combine(_environment.ContentRootPath, "upload", folder, subFolder);
            var files = new System.IO.DirectoryInfo(filePath);
            foreach (var i in files.GetFiles())
            {
                _files.Add(new File(i.Name, GetFileUrl(i.Name, folder, subFolder), i.Extension));
            }

            return await Task.Run(() =>
            {
                return _files;
            });
        }

        public async Task SaveFiles(List<IFormFile> files, string folder, string subFolder)
        {
            foreach (var file in files)
            {
                string fileFormat = GetFileFormat(file.FileName);
                string fileName = GenerateNewFileName() + fileFormat.ToLower();

                byte[] bytesFile = ConvertFileInByteArray(file);

                string directory = CreateFilePath(fileName, folder, subFolder);
                await System.IO.File.WriteAllBytesAsync(directory, bytesFile);

                var url = GetFileUrl(fileName, folder, subFolder);
                _files.Add(new File(
                    fileName,
                    url,
                    fileFormat));
            }
        }

        private string GetFileFormat(string fullFileName)
        {
            var format = fullFileName.Split(".").Last();
            return "." + format;
        }

        private string GenerateNewFileName()
        {
            return Guid.NewGuid().ToString().ToLower();
        }

        private string CreateFilePath(string fileName, string folder, string subFolder)
        {
            var filePath = System.IO.Path.Combine(_environment.ContentRootPath, "upload", folder, subFolder);
            if (!System.IO.Directory.Exists(filePath))
                System.IO.Directory.CreateDirectory(filePath);

            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), filePath, fileName);
        }

        private string GetFileUrl(string fileName, string folder, string subFolder)
        {
            var request = _context.HttpContext.Request;
            return System.IO.Path.Combine(request.Host.Value, "upload", folder, subFolder, fileName);
        }

        public void RemoveFilesFolder(string folder, string subFolder)
        {
            var filePath = System.IO.Path.Combine(_environment.ContentRootPath, "upload", folder, subFolder);
            var files = new System.IO.DirectoryInfo(filePath);
            foreach (var i in files.GetFiles())
            {
               i.Delete();
            }
        }

        private byte[] ConvertFileInByteArray(IFormFile file)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}