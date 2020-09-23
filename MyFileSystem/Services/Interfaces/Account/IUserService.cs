using MyFileSystem.Core.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFileSystem.Services.Interfaces.Account
{
    public interface IUserService
    {
        Task<object> Login(LoginDto loginDto);
    }
}
