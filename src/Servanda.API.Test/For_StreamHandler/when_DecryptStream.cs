using Servanda.API.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_DecryptStream : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private readonly MemoryStream _streamToDecrypt;
        private MemoryStream _decryptedStream;

        public when_DecryptStream()
        {
            _streamHandler = new StreamHandler();
            _streamToDecrypt = new MemoryStream(new byte[8] { 44, 197, 89, 185, 143, 159, 49, 180 });
        }

        [Fact]
        public async void It_should_decrypt_encrypted_stream()
        {
            _decryptedStream = await _streamHandler.DecryptData(_streamToDecrypt);
            var decryptedvalue = Encoding.UTF8.GetString(_decryptedStream.ToArray());
            Assert.Equal("Servanda", decryptedvalue);
        }

        public void Dispose()
        {
            _streamToDecrypt.Dispose();
            _decryptedStream.Dispose();
        }
    }
}
