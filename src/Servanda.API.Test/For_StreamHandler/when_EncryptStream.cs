using Servanda.API.Repositories;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_EncryptStream : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private readonly MemoryStream _streamToEncrypt;
        private MemoryStream _encryptedStream;
        private byte[] _dataToEncrypt;

        public when_EncryptStream()
        {
            _dataToEncrypt = Encoding.UTF8.GetBytes("Servanda");
            _streamToEncrypt = new MemoryStream(_dataToEncrypt);
            _streamHandler = new StreamHandler();
        }

        [Fact]
        public async void Encrypted_data_should_not_equal_source_data()
        {
            _encryptedStream = await _streamHandler.EncryptData(_streamToEncrypt);
            var encryptedData = _encryptedStream.ToArray();
            Assert.NotEqual(_dataToEncrypt, encryptedData);
        }

        public void Dispose()
        {
            _streamToEncrypt.Dispose();
            _encryptedStream.Dispose();
        }
    }
}
