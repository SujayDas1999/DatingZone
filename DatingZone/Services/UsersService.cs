using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Repository;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;

        public UsersService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<MemberDto>> GetAllUsers()
        {
            var users = await this.userRepository.GetMembersAsync();
            return users;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await this.userRepository.GetUserByIdAsync(id);
        }

        public async Task<MemberDto> GetUserByUserName(string name)
        {
            var user = await this.userRepository.GetMemberAsync(name);
            return user;
        }
    }
}
