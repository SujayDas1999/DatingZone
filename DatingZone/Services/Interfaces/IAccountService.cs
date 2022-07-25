using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatingZone.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto);
        Task<ActionResult<UserDto>> LoginUser(LoginDto loginDto);
    }
}
