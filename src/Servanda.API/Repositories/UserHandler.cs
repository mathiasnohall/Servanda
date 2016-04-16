using System;
using System.Threading.Tasks;

namespace Servanda.API.Repositories
{
    public interface IUserHandler
    {
        Task AddFileToUserMapping(string userId, string fileId);
    }

    public class UserHandler : IUserHandler
    {
        public Task AddFileToUserMapping(string userId, string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
