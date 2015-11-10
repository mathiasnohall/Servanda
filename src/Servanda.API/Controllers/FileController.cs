using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Servanda.API.Repositories;
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
            await _fileHandler.HandleUpload(file);            

            return View();
        }
    }
}
