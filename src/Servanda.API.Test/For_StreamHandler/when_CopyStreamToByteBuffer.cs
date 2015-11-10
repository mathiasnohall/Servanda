using Servanda.API.Repositories;
using System;
using System.IO;
using Xunit;

namespace Servanda.API.Test.For_StreamHandler
{
    public class when_CopyStreamToByteBuffer : IDisposable
    {
        private readonly IStreamHandler _streamHandler;
        private readonly Stream _stream;
        private byte[] _result;

        public when_CopyStreamToByteBuffer()
        {
            _streamHandler = new StreamHandler();
            var bytearray = new byte[666];
            _stream = new MemoryStream(bytearray);
        }

        [Fact]
        public async void It_should_copy_stream_to_memorystream()
        {
            _result = await _streamHandler.CopyStreamToByteBuffer(_stream);
            Assert.Equal(_result.Length, 666);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
