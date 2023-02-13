using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Repository
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<ServiceResponse<AppUser>> GetUserByUserNameAsync(string name);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);

        Task<bool> UpdateUser(AppUser appUser);

    }
}
