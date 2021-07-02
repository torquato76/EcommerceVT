using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceVT.Services
{
    public class InviteService : IInvite
    {
        private readonly AppDbContext _context;
        private readonly IAuthenticate _authenticate;

        public InviteService(AppDbContext context, IAuthenticate authenticate)
        {
            _context = context;
            _authenticate = authenticate;
        }

        public async Task<Invite> Get(int inviteId)
        {
            return await _context.Invite.AsNoTracking().
                                         FirstOrDefaultAsync(x => x.Invite_Id == inviteId &&
                                                             x.User_Id == int.Parse(_authenticate.GetClaimValue("UserId")));
        }

        public async Task<IList<Invite>> GetAll(int userId)
        {
            return await _context.Invite.Where(x => x.User_Id == userId).ToListAsync();
        }

        public async Task<Invite> Post(Invite invite)
        {
            try
            {
                _context.Add(invite);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return invite;
        }

        public async Task<Invite> Put(Invite invite)
        {
            _context.Entry(invite).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return invite;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}