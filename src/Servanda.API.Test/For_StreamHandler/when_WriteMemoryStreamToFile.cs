using Servanda.API.Repositories;
using System;
using System.IO;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_WriteMemoryStreamToFile : IDisposable
    {
        protected readonly IStreamHandler _streamHandler;
        protected readonly byte[] _buffer;
        private readonly string _unitTestFilePath;

        public when_WriteMemoryStreamToFile()
        {
            _unitTestFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            _unitTestFilePath += "awdawd.data";
            _buffer = new byte[80000000]; // 80 MB
            _streamHandler = new StreamHandler();
        }       

        [Fact]
        public void It_should_write_memorystream_to_disk()
        {
            _streamHandler.WriteBufferToFile(_buffer, _unitTestFilePath);

            Assert.True(File.Exists(_unitTestFilePath));
        }

        public void Dispose()
        {
            File.Delete(_unitTestFilePath);
        }
    }
}
