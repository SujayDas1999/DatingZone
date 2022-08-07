using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DatingZone.Exceptions;

namespace DatingZone.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, IAccountService accountService, ITokenService tokenService)
        {
            _context = context;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
        {
            try
            {
                return await _accountService.RegisterUser(registerDto);
            }
            catch(UserExists)
            {
                return BadRequest("User already exists");
            }
            catch(UserUnautorized)
            {
                return Unauthorized("Invalid User name or Password");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> LoginUser(LoginDto loginDto)
        {
            try
            {
                return await _accountService.LoginUser(loginDto);
            }
            catch(PropertyMissing)
            {
                return BadRequest("Proprty missing");
            }
            catch(UserUnautorized)
            {
                return Unauthorized("Invalid UserName or Password");
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
