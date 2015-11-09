﻿using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Servanda.API.Repositories
{
    public interface IFileUploadHandler
    {
        Task HandleUpload(IFormFile file);
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

        public async Task HandleUpload(IFormFile file)
        {
            var memoryStream =  await _streamHandler.CopyToMemoryStream(file.OpenReadStream());

            // cryptostream?

            await Task.Run(() => _streamHandler.WriteMemoryStreamToFile(memoryStream, _hostingEnvironment.WebRootPath + "uploads"));
        }
    }
}