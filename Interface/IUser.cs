using EcommerceVT.Model;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IUser : IDisposable
    {
        Task<User> Get(int userId);
        Task<User> GetByEmail(string email);
        Task<User> Post(User user);
        Task<User> Put(User user);
    }
}
