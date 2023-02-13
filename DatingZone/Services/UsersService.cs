using AutoMapper;
using DatingZone.Data;
using DatingZone.Entities;
using DatingZone.Entities.Dtos;
using DatingZone.Repository;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingZone.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersService(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<IEnumerable<MemberDto>> GetAllUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return users;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<MemberDto> GetUserByUserName(string name)
        {
            var user = await _userRepository.GetMemberAsync(name);
            return user;
        }

        public async Task<ServiceResponse<AppUser>> FetchUserByUserName(string username)
        {
            return await _userRepository.GetUserByUserNameAsync(username);
        }

        public async Task<ServiceResponse<bool>> UpdateUserByUsername(string username, MemberEditDto memberEditDto)
        {

            var member = await _userRepository.GetUserByUserNameAsync(username);

            if (member.Status == 404)
            {
                return new ServiceResponse<bool>
                {
                    Message = member.Message,
                    Status = member.Status,
                    Success = member.Success,
                };
            }

            var mappedUser = _mapper.Map(memberEditDto,member.Data);


           var result = await _userRepository.UpdateUser(mappedUser);

           if(result)
            {
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success",
                    Status = 204,
                    Success = true
                };
            }

            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Some error happened",
                Status = 400

            };


        }

        public async Task<ServiceResponse<PhotoDto>> SavePhotosAsync(IFormFile file, string username)
        {
            var user = await FetchUserByUserName(username);

            if (user.Success == false)
            {
                return new ServiceResponse<PhotoDto>
                {
                    Message = user.Message,
                    Status = user.Status,
                    Success = user.Success
                };
            }

            AppUser member = user.Data;

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return new ServiceResponse<PhotoDto>
            {
                Message = result.Error.Message,
                Status = 400,
                Success = false
            };

            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = false,
            };

            if (member.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            member.Photos.Add(photo);



            if (await _userRepository.SaveAllAsync()) 
            {
                var photoDto = _mapper.Map<PhotoDto>(photo);

                return new ServiceResponse<PhotoDto>
                {
                    Data = photoDto,
                    Status = 200,
                    Success = true,
                    Message = "Successfully updated photo"
                };
            } 

            return new ServiceResponse<PhotoDto>
            {
                Message = "Some error occured",
                Status = 400,
                Success = false
            };
        }
    }
}
