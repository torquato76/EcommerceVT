using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using EcommerceVT.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly ILocation _location;

        public UserService(AppDbContext context, ILocation location)
        {
            _context = context;
            _location = location;
        }

        public async Task<User> Get(int userId)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.User_Id == userId);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public async Task<User> Post(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Put(User user)
        {

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
