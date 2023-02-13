using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingZone.Helpers;
using DatingZone.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DatingZone.Services
{
    public class PhotoService: IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResults = new ImageUploadResult();

            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uplaodParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName,stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "da-net"
                };

                uploadResults = await _cloudinary.UploadAsync(uplaodParams);

            }

            return uploadResults;

        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deletionParams = new DeletionParams(null);
            
            return _cloudinary.DestroyAsync(deletionParams);
            
        }
    }
}
