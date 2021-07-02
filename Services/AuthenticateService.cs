using EcommerceVT.Data;
using EcommerceVT.Interface;
using EcommerceVT.Model;
using EcommerceVT.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVT.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly AppDbContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _accessor;
        public AuthenticateService(AppDbContext context,
            IConfiguration Configuration, IHttpContextAccessor accessor)
        {
            _context = context;
            _config = Configuration;
            _accessor = accessor;
        }

        public string Email => _accessor.HttpContext.User.Identity.Name;
        public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

        public async Task<User> Login(Authenticate authenticate)
        {
            authenticate.Password = new CryptoString().EncryptText(authenticate.Password);

            return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Login == authenticate.Login &&
                                                                               x.Password == authenticate.Password);
        }

        public string CreateTokenJWT(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.User_Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: expiry, signingCredentials: credentials, claims: claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public string GetClaimValue(string nameClaim)
        {
            return GetClaimsIdentity().FirstOrDefault(a => a.Type == nameClaim)?.Value;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
