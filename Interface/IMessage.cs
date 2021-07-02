using EcommerceVT.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IMessage : IDisposable
    {
        Task<Messages> Get(int messageId);
        Task<IList<Messages>> GetAll(int dealId);
        Task<int> Post(Messages message);
        Task<int> Put(Messages message);
    }
}
