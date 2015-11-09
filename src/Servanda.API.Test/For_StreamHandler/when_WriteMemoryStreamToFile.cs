using Servanda.API.Repositories;
using System;
using System.IO;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_WriteMemoryStreamToFile : IDisposable
    {
        protected readonly IStreamHandler _streamHandler;
        protected readonly MemoryStream _stream;
        private readonly string _unitTestFilePath;

        public when_WriteMemoryStreamToFile()
        {
            _unitTestFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            _unitTestFilePath += "awdawd.data";
            _stream = new MemoryStream(new byte[80000000]); // 80 MB
            _streamHandler = new StreamHandler();
        }
        

        public void Dispose()
        {
            File.Delete(_unitTestFilePath);
            _stream.Dispose();
        }

        [Fact]
        public void Test()
        {
            _streamHandler.WriteMemoryStreamToFile(_stream, _unitTestFilePath);
            
            Assert.True(File.Exists(_unitTestFilePath));
        }

    }
}
