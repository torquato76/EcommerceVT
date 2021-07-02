using EcommerceVT.Model;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface ILocation :  IDisposable
    {
        Task<Location> Get(int locationId);
        Task<Location> Post(Location location);
        Task<int> Put(Location location);
    }
}
