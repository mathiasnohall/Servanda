using Servanda.API.Repositories;
using System;
using System.IO;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_WriteMemoryStreamToFile : IDisposable
    {
        protected readonly IStreamHandler _streamHandler;
        protected readonly MemoryStream _memoryStream;
        private readonly string _unitTestFilePath;

        public when_WriteMemoryStreamToFile()
        {
            _unitTestFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            _unitTestFilePath += "awdawd.data";
            _memoryStream = new MemoryStream(new byte[80000000]); // 80 MB
            _streamHandler = new StreamHandler();
        }
        

        public void Dispose()
        {
            File.Delete(_unitTestFilePath);
            _memoryStream.Dispose();
        }

        [Fact]
        public void It_should_write_memorystream_to_disk()
        {
            _streamHandler.WriteMemoryStreamToFile(_memoryStream, _unitTestFilePath);
            
            Assert.True(File.Exists(_unitTestFilePath));
        }

    }
}
