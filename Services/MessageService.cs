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
    public class MessageService : IMessage
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Messages> Get(int messageId)
        {
            return await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Message_Id == messageId);
        }

        public async Task<IList<Messages>> GetAll(int dealId)
        {
            return await _context.Messages.Where(x => x.Deal_Id == dealId).OrderBy(x => x.Deal_Id).ToListAsync();
        }

        public async Task<int> Post(Messages message)
        {
            _context.Add(message);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Put(Messages message)
        {
            _context.Entry(message).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}