using EcommerceVT.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IBid : IDisposable
    {
        Task<Bid> Get(int bidId);
        Task<IList<Bid>> GetAll(int dealId);
        Task<int> Post(Bid bid);
        Task<int> Put(Bid bid);
    }
}
