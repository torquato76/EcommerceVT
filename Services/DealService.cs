using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Services
{
    public class DealService : IDeal
    {
        private readonly AppDbContext _context;
        private readonly IAuthenticate _authenticate;

        public DealService(AppDbContext context,  IAuthenticate authenticate)
        {
            _context = context;
            _authenticate = authenticate;
        }

        public async Task<Deal> Get(int dealId)
        {
            return await _context.Deal.AsNoTracking().FirstOrDefaultAsync(x => x.Deal_Id == dealId);
        }

        public async Task<int> Post(Deal deal)
        {
            var userId = _authenticate.GetClaimValue("UserId");

            deal.User_Id = int.Parse(userId);

            _context.Add(deal);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Put(Deal deal)
        {
            var userId = _authenticate.GetClaimValue("UserId");

            deal.User_Id = int.Parse(userId);

            _context.Entry(deal).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}