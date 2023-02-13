using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<MemberDto>> GetAllUsers();
        Task<AppUser> GetUserById(int id);
        Task<MemberDto> GetUserByUserName(string name);
        Task<ServiceResponse<bool>> UpdateUserByUsername(string username,MemberEditDto memberEditDto);
        Task<ServiceResponse<AppUser>> FetchUserByUserName(string username);
        Task<ServiceResponse<PhotoDto>> SavePhotosAsync(IFormFile file, string username);
    }
}
