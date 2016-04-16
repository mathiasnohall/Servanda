using FakeItEasy;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Servanda.API.Repositories;
using System.IO;
using Xunit;

namespace Servanda.API.Test.For_FileUploadHandler
{
    public class when_handleUpload
    {
        protected static IFileUploadHandler _fileUploadHandler;
        protected static IStreamHandler _streamHandler;
        protected static IHostingEnvironment _hostingEnvironment;

        public when_handleUpload()
        {        
            _streamHandler = A.Fake<IStreamHandler>();
            _hostingEnvironment = A.Fake<IHostingEnvironment>();

            _fileUploadHandler = new FileUploadHandler(_streamHandler, _hostingEnvironment);
        }

        [Fact]
        public void for_FileUploadHandler_when_fileupload_it_should_encrypt_the_file_write_it_to_storage_and_return_a_uniqe_filename()
        {
            var fileId = _fileUploadHandler.HandleUpload(A.Fake<IFormFile>());

            A.CallTo(() => _streamHandler.CopyStreamToByteBuffer(A<Stream>.Ignored)).MustHaveHappened();

            A.CallTo(() => _streamHandler.EncryptData(A<byte[]>.Ignored)).MustHaveHappened();

            A.CallTo(() => _streamHandler.WriteBufferToFile(A<byte[]>.Ignored, A<string>.Ignored)).MustHaveHappened();

            Assert.NotNull(fileId);
                
        }


    }
}
