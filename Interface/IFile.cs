using EcommerceVT.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IFile
    {
        Task SaveFiles(List<IFormFile> files, string folder, string subFolder);
        Task<IList<File>> GetFiles(string folder, string subFolder);
        void RemoveFilesFolder(string folder, string subFolder);
    }
}