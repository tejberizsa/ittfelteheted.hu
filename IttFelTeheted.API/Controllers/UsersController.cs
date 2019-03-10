using System;
using System.Collections.Generic;
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

namespace IttFelTeheted.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IApplicationRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            PageParams userParams = new PageParams();
            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("followed")]
        public async Task<IActionResult> GetFollowedUsers([FromQuery]PageParams userParams)
        {
            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            userToReturn.IsFollowedByCurrentUser = user.Follower.Any(f => f.FollowerId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();
            
            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{userId}/follow/{followedId}")]
        public async Task<IActionResult> FollowUser(int userId, int followedId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFollow = await _repo.GetUserFollow(userId, followedId);

            if (userFollow != null)
                return BadRequest("Már követed");

            var followed = await _repo.GetUser(followedId);
            if (followed == null)
                return NotFound();
            
            userFollow = new UserFollow
            {
                FollowerId = userId,
                FollowedId = followedId
            };
            _repo.Add<UserFollow>(userFollow);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Nem sikerült a feliratkozás");
        }

        [HttpPost("{userId}/unfollow/{followedId}")]
        public async Task<IActionResult> UnfollowUser(int userId, int followedId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFollow = await _repo.GetUserFollow(userId, followedId);

            if (userFollow == null)
                return BadRequest("Nem követed ezt a felhasználót");

            _repo.Delete<UserFollow>(userFollow);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Nem sikerült a leiratkozás");
        }
    }
}