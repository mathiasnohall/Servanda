using Servanda.API.Repositories;
using System;
using System.Text;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_encrypt_then_decrypt : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private readonly byte[] _nonEncryptedSourceData;

        public when_encrypt_then_decrypt()
        {
            _streamHandler = new StreamHandler();

            _nonEncryptedSourceData = Encoding.UTF8.GetBytes("Servanda");
        }

        [Fact]
        public async void Encrypted_stream_should_be_decryptable()
        {
            var encryptedData = await _streamHandler.EncryptData(_nonEncryptedSourceData);

            var decryptedData = await _streamHandler.DecryptData(encryptedData);

            Assert.Equal(_nonEncryptedSourceData, decryptedData);
        }

        public void Dispose()
        {
        }
    }
}
