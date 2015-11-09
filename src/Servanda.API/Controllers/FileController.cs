using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Dnx.Runtime;
using Microsoft.Net.Http.Headers;
using Servanda.API.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Servanda.API.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileUploadHandler _fileHandler;

        public FileController(IFileUploadHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //var uploads = Path.Combine(_hostingEnvironment.ApplicationBasePath, "uploads");
            //foreach (var file in files)
            //{
            //    if (file.Length > 0)
            //    {
            //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //        await file.SaveAsAsync(Path.Combine(uploads, fileName));
            //    }
            //}


            await _fileHandler.HandleUpload(file);            

            return View();
        }
    }
}
