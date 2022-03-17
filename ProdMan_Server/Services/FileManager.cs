using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProdMan_Server.Services
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment env;
        public string RootPhysicalPath { get; set; }

        public FileManager(IWebHostEnvironment env)
        {
            this.env = env;
            RootPhysicalPath = env.WebRootPath + "\\CabinImages\\";
            if (!Directory.Exists(RootPhysicalPath))
            {
                Directory.CreateDirectory(RootPhysicalPath);
            }
        }
        public bool DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return true;
            }
            return false;
        }

        public async Task<string> SaveFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            string NewFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            bool success = false;

            using (FileStream fs = new(RootPhysicalPath + NewFileName, FileMode.Create))
            {
                try
                {
                    await file.OpenReadStream().CopyToAsync(fs);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
            }
            if (!success)
            {
                return null;
            }
            else
            {
                return "/CabinImages/" + NewFileName;
            }
        }
    }
}
