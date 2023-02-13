using AutoMapper;
using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Extensions;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingZone.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUsersService usersService, IMapper mapper, IPhotoService photoService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);


            return Ok(usersToReturn);
        }

        [HttpGet("userid/{id}")]
        public async Task<ActionResult<MemberDto>> GetUserById(int id)
        {
            return Ok(await _usersService.GetUserById(id));
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
        {
            var user = await _usersService.GetUserByUserName(username);

            return Ok(user);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser(MemberEditDto memberEditDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(username)) return Unauthorized("Unauthorized");

            var result = await _usersService.UpdateUserByUsername(username, memberEditDto);

            if(result.Status == 404)
            {
                return NotFound($"No user found with username {username}");
            }

            return NoContent();
           
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<ServiceResponse<PhotoDto>>> AddPhotosAsync(IFormFile file)
        {
            var username = User.GetUsername();

            if (string.IsNullOrEmpty(username)) return Unauthorized("Unauthorized");

            return await _usersService.SavePhotosAsync(file,username);

        }
    }
}
    