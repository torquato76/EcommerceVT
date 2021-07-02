using EcommerceVT.Model;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IDeal : IDisposable
    {
        Task<Deal> Get(int dealId);
        Task<int> Post(Deal deal);
        Task<int> Put(Deal deal);
    }
}
