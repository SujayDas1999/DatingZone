using AutoMapper;
using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersService _usersService;
        private readonly IMapper mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();

            var usersToReturn = this.mapper.Map<IEnumerable<MemberDto>>(users);


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
    }
}
    