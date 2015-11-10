using Servanda.API.Repositories;
using System;
using System.Text;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_EncryptData : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private byte[] _encryptedData;
        private byte[] _dataToEncrypt;

        public when_EncryptData()
        {
            _dataToEncrypt = Encoding.UTF8.GetBytes("Servanda");
            _streamHandler = new StreamHandler();
        }

        [Fact]
        public async void Encrypted_data_should_not_equal_source_data()
        {
            _encryptedData = await _streamHandler.EncryptData(_dataToEncrypt);
            Assert.NotEqual(_dataToEncrypt, _encryptedData);
        }

        public void Dispose()
        {
        }
    }
}
