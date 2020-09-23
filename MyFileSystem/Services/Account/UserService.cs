using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFileSystem.Core.DTOs.Account;
using MyFileSystem.Core.Entities;
using MyFileSystem.Services.Interfaces.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFileSystem.Services.Account
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;

        public UserService(SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _config = config;
        }
        public async Task<object> Login(LoginDto loginDto)
        {
            var islogin = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
            if (!islogin.Succeeded) throw new Exception("Invalid user name or password");
              
            return new { Token = GenerateJSONWebToken(loginDto) };
        }

        private string GenerateJSONWebToken(LoginDto user)
        {
           

            var signingCredentials = new SigningCredentials(  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:SecurityKey"])),SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
