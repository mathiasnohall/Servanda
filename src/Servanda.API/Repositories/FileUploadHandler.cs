using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace Servanda.API.Repositories
{
    public interface IFileUploadHandler
    {
        Task<string> HandleUpload(IFormFile file);
    }

    public class FileUploadHandler : IFileUploadHandler
    {
        private readonly IStreamHandler _streamHandler;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileUploadHandler(IStreamHandler streamHandler, IHostingEnvironment hostingEnvironment)
        {
            _streamHandler = streamHandler;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> HandleUpload(IFormFile file)
        {
            var data = await _streamHandler.CopyStreamToByteBuffer(file.OpenReadStream());

            var encryptedData = await _streamHandler.EncryptData(data);

            var uniqueFileName = Guid.NewGuid().ToString();

            await _streamHandler.WriteBufferToFile(encryptedData, _hostingEnvironment.WebRootPath + "uploads/" + uniqueFileName);

            return uniqueFileName;
        }
    }
}
