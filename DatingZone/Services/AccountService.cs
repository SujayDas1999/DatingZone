using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingZone.Exceptions;

namespace DatingZone.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;
        public AccountService(DataContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        public async Task<ActionResult<UserDto>> LoginUser(LoginDto loginDto)
        {
            var user = await this.context.tblUsers.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) throw new UserUnautorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            
           for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) throw new UserUnautorized("Invalid Password");
            }

            return new UserDto
            {
                UserName = loginDto.UserName,
                Token = this.tokenService.CreateToken(user)
            };

        }

        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
        {
            var isUserExists = await UserExists(registerDto.UserName, registerDto.EmailAddress);

            if (isUserExists) throw new UserExists("User exists");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                EmailAddress = registerDto.EmailAddress,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };


            this.context.Add(user);

            await this.context.SaveChangesAsync();

            return new UserDto
            {
                UserName = registerDto.UserName,
                Token = this.tokenService.CreateToken(user)
            };
            
        }

        private async Task<bool> UserExists(string username, string email)
        {
            return await this.context.tblUsers.AnyAsync(x => x.UserName.ToLower() == username || x.EmailAddress.ToLower() == email);
        }

    }
}
