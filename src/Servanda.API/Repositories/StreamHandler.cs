using System;
using System.IO;
using System.Threading.Tasks;

namespace Servanda.API.Repositories
{
    public interface IStreamHandler
    {
        Task<MemoryStream> CopyToMemoryStream(Stream stream);

        Task<MemoryStream> EncryptStream(MemoryStream memoryStream);

        void WriteMemoryStreamToFile(MemoryStream stream, string path);
    }

    public class StreamHandler : IStreamHandler
    {
        public async Task<MemoryStream> CopyToMemoryStream(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream;            
        }

        public Task<MemoryStream> EncryptStream(MemoryStream memoryStream)
        {
            throw new NotImplementedException();
        }

        public void WriteMemoryStreamToFile(MemoryStream memoryStream, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (memoryStream)
            {
                byte[] bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                fileStream.Write(bytes, 0, bytes.Length);
            };

        }
    }
}



