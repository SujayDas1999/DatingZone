using DatingZone.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers();
        Task<ActionResult<AppUser>> GetUserById(int id);
    }
}
