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
    public class BidService : IBid
    {
        private readonly AppDbContext _context;

        public BidService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bid> Get(int bidId)
        {
            return await _context.Bid.AsNoTracking().FirstOrDefaultAsync(x => x.Bid_Id == bidId);
        }

        public async Task<IList<Bid>> GetAll(int dealId)
        {
            return await _context.Bid.Where(x => x.Deal_Id == dealId).OrderBy(x => x.Deal_Id).ToListAsync();
        }

        public async Task<int> Post(Bid bid)
        {
            _context.Add(bid);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Put(Bid bid)
        {
            _context.Entry(bid).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
