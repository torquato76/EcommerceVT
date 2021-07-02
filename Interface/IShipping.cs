using EcommerceVT.Model;
using System;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IShipping : IDisposable
    {
        Task<Shipping> Get(int zipCodeSender, int zipCodeRecipient, int productWeight = 1,
            int productLength = 20, int productheight = 10, int productWidth = 20, int productValue = 0);
    }
}
