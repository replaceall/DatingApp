using DatingApp.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        Task<UserModel> Register(UserModel user, string password);
        Task<UserModel> LogIn(string user, string password);
        Task<bool> UserExists(string username);
    }
}
