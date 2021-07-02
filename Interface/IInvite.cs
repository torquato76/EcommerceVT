using EcommerceVT.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IInvite : IDisposable
    {
        Task<Invite> Get(int inviteId);
        Task<IList<Invite>> GetAll(int userId);
        Task<Invite> Post(Invite invite);
        Task<Invite> Put(Invite invite);
    }
}
