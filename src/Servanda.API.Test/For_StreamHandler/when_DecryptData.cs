using Servanda.API.Repositories;
using System;
using System.Text;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_DecryptData : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private readonly byte[] _encryptedData;

        public when_DecryptData()
        {
            _streamHandler = new StreamHandler();
            _encryptedData = new byte[16] { 44, 197, 89, 185, 143, 159, 49, 180, 117, 157, 67, 253, 45, 45, 24, 8 };
        }

        [Fact]
        public async void It_should_decrypt_encrypted_stream()
        {
            var decryptedData = await _streamHandler.DecryptData(_encryptedData);
            var decryptedvalue = Encoding.UTF8.GetString(decryptedData);
            Assert.Equal("Servanda", decryptedvalue);
        }

        public void Dispose()
        {
        }
    }
}
