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
    public class when_encrypt_then_decrypt : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private readonly MemoryStream _nonEncryptedSourceStream;
        private readonly byte[] _nonEncryptedSourceData;

        public when_encrypt_then_decrypt()
        {
            _streamHandler = new StreamHandler();

            _nonEncryptedSourceData = Encoding.UTF8.GetBytes("Servanda");
            _nonEncryptedSourceStream = new MemoryStream(_nonEncryptedSourceData);

        }

        [Fact]
        public async void Encrypted_stream_should_be_decryptable()
        {
            var encryptedStream = await _streamHandler.EncryptData(_nonEncryptedSourceStream);
            var deCryptedStream = await _streamHandler.DecryptData(encryptedStream);
            var deCryptedData = Encoding.UTF8.GetString(deCryptedStream.ToArray());

            Assert.Equal(_nonEncryptedSourceData, deCryptedStream.ToArray());
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
