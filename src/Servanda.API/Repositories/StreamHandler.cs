using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servanda.API.Repositories
{
    public interface IStreamHandler
    {
        Task<byte[]> CopyStreamToByteBuffer(Stream stream);

        Task<byte[]> EncryptData(byte[] buffer);

        Task<byte[]> DecryptData(byte[] buffer);

        void WriteBufferToFile(byte[] buffer, string path);
    }

    public class StreamHandler : IStreamHandler
    {
        private readonly DESCryptoServiceProvider _cryptor;

        private readonly string _cryptoKey; // consider removing this var to somewhere more secret place
        private readonly string _IV; // consider removing this var to somewhere more secret place

        public StreamHandler()
        {
            _cryptoKey = "SERVANDA"; // obviously change this in the future :)
            _IV = "SERVANDA";

            _cryptor = new DESCryptoServiceProvider();
            _cryptor.Key = Encoding.ASCII.GetBytes(_cryptoKey);
            _cryptor.IV = Encoding.ASCII.GetBytes(_IV);
        }

        public async Task<byte[]> CopyStreamToByteBuffer(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        public Task<byte[]> DecryptData(byte[] buffer)
        {
            return Task.Run<byte[]>(() =>
            {
                using (var encryptor = _cryptor.CreateDecryptor())
                {
                    return PerformCryptography(encryptor, buffer);
                }
            });
        }

        public Task<byte[]> EncryptData(byte[] buffer)
        {
           return Task.Run<byte[]>(() =>
           {
               using (var encryptor = _cryptor.CreateEncryptor())
               {
                   return PerformCryptography(encryptor, buffer);
               }
           });
        }

        private byte[] PerformCryptography(ICryptoTransform cryptoTransform, byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        // todo refactor
        public void WriteBufferToFile(byte[] buffer, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var memoryStream = new MemoryStream(buffer))
            {
                byte[] bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                fileStream.Write(bytes, 0, bytes.Length);
            };

        }
    }
}



