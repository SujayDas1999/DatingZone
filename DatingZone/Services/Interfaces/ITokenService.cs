using DatingZone.Entities;

namespace DatingZone.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
