using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingZone.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.tblUsers
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.tblUsers
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.tblUsers.FindAsync(id);
        }

        public async Task<ServiceResponse<AppUser>> GetUserByUserNameAsync(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return new ServiceResponse<AppUser>
                {
                    Message = "Empty Param",
                    Status = 400,
                    Success = false
                };
            }

            var user = await _context.tblUsers
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == name);

            if(user == null)
            {
                return new ServiceResponse<AppUser>
                {
                    Message = $"No user found with username {name}",
                    Status = 404,
                    Success = false
                };
            }

            return new ServiceResponse<AppUser>
            {
                Data = user,
                Status = 200,
                Success = true,
                Message = $"Successfully fetched user with username {name}"
            };
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.tblUsers
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(AppUser appUser)
        {

            if(await SaveAllAsync())
            {
                return true;
            }

            return false;

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
