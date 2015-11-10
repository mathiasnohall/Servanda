using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servanda.API.Repositories
{
    public interface IStreamHandler
    {
        Task<MemoryStream> CopyToMemoryStream(Stream stream);

        Task<MemoryStream> EncryptData(MemoryStream sourceStream);

        Task<MemoryStream> DecryptData(MemoryStream sourceStream);

        void WriteMemoryStreamToFile(MemoryStream memoryStream, string path);
    }

    public class StreamHandler : IStreamHandler
    {
        private readonly string _cryptoKey; // consider removing this var to somewhere more secret place
        private readonly string _IV; // consider removing this var to somewhere more secret place

        public StreamHandler()
        {
            _cryptoKey = "SERVANDA"; // obviously change this in the future :)
            _IV = "SERVANDA"; 
        }

        public async Task<MemoryStream> CopyToMemoryStream(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream;            
        }

        public async Task<MemoryStream> DecryptData(MemoryStream sourceStream)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = Encoding.ASCII.GetBytes(_cryptoKey);
            cryptic.IV = Encoding.ASCII.GetBytes(_IV);

            var memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptic.CreateDecryptor(), CryptoStreamMode.Write);




            return null;
        }

        public async Task<MemoryStream> EncryptData(MemoryStream sourceStream)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = Encoding.ASCII.GetBytes(_cryptoKey);
            cryptic.IV = Encoding.ASCII.GetBytes(_IV);

            var memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);

            var buffer = sourceStream.ToArray();
            sourceStream.Dispose();
            await cryptoStream.WriteAsync(buffer, 0, buffer.Length);
            return memoryStream;
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



