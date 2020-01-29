using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _datacontext;

        public AuthRepository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        public async Task<UserModel> LogIn(string username, string password)
        {
            var user = await _datacontext.tblUser.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i] ) return false;
                }
            }
            return true;
        }

        public async Task<UserModel> Register(UserModel user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatepasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _datacontext.tblUser.AddAsync(user);
            await _datacontext.SaveChangesAsync();

            return user;
        }

        private void CreatepasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _datacontext.tblUser.AnyAsync(x => x.UserName == username))
                 return true;
            return false;
        }
    }
}
