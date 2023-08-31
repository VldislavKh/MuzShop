using Domain.Entities;
using Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.Interfaces.ServiceInterfaces;
using MuzShop.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MuzShop.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthService(ApplicationContext context, IOptions<AuthOptions> options)
        {
            _authOptions = options;
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user =  await _context.Users.SingleOrDefaultAsync(u => u.Name == username && u.Password == CreateSHA256(password))
                ?? throw new NotFoundException(nameof(_context.Users), username);

            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == user.RoleId)
                ?? throw new NotFoundException(nameof(_context.Roles), user.RoleId);

            user.Role = role;

            return user;
        }

        private string CreateSHA256(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }

        public string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var role = _context.Roles.SingleOrDefault(r => r.Id == user.RoleId);
            claims.Add(new Claim("role", role.Name.ToString()));

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
