using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IttFelTeheted.API.Data;
using IttFelTeheted.API.Dtos;
using IttFelTeheted.API.Helpers;
using IttFelTeheted.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace IttFelTeheted.API.Controllers
{
    [Route("api/posts/{postId}/photos")]
    [ApiController]
    public class PostPhotoController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        public PostPhotoController(IApplicationRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }
        
        [HttpGet("{id}", Name = "GetPostedPhoto")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostedPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPostedPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(LogUserActivity))]
        public async Task<IActionResult> AddPhoto(int postId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var postFromRepo = await _repo.GetPostByID(postId);
            if (postFromRepo.User.Id != userId)
                return Unauthorized();

            var file = photoForCreationDto.File;
            var path = $"C:\\PostPhotos\\";
            var filename = $"{postId}_{DateTime.Now.ToString("yyMMddHHmmssff")}";
            var extension = ".jpg";

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (Image<Rgba32> image = Image.Load(stream))
                    {
                        image.Mutate(x => x.Resize(1024, 0));
                        image.Save(path + filename + extension);
                    }
                }
            }
            
            photoForCreationDto.Url = $"https://ittfelteheted.hu/api/posts/{postId}/photos/link/{filename}";
            var photo = _mapper.Map<PostedPhoto>(photoForCreationDto);

            if (!postFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            postFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPostedPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Fénykép feltöltés nem sikerült");
        }

        [HttpGet("link/{url}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhoto(string url)
        {
            var path = $"C:\\PostPhotos\\";
            var extension = ".jpg";
            var image = await Task.Run(() => System.IO.File.OpenRead(path + url + extension));
            return File(image, "image/jpeg");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPostedPhoto(id);
            if(photoFromRepo == null)
                return BadRequest("Kép nem található");
                
            var user = await _repo.GetUser(userId);
            if(photoFromRepo.Post.User.Id != userId)
                return Unauthorized();

            var path = $"C:\\PostPhotos\\";
            var extension = ".jpg";
            var fileName = photoFromRepo.Url.Split("/").Last();
            await Task.Run(() => System.IO.File.Delete(path + fileName + extension));
            _repo.Delete(photoFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Kép törlése nem sikerült");
        }
    }
}