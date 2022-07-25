using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;

        public UsersService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            return await _context.tblUsers.ToListAsync();
        }

        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            return await _context.tblUsers.FindAsync(id);
        }
    }
}
