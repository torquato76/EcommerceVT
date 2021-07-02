using EcommerceVT.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceVT.Interface
{
    public interface IAuthenticate : IDisposable
    {
        Task<User> Login(Authenticate authenticate);
        string CreateTokenJWT(User user);

        IEnumerable<Claim> GetClaimsIdentity();
        string GetClaimValue(string nameClaim);
    }
}
