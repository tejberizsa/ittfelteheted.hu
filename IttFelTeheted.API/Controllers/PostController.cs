using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IttFelTeheted.API.Data;
using IttFelTeheted.API.Dtos;
using IttFelTeheted.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace IttFelTeheted.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        public PostController(IApplicationRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _repo.GetPosts();

            var postsToReturn = _mapper.Map<IEnumerable<PostForListDto>>(posts);

            return Ok(postsToReturn);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _repo.GetPostByID(id);

            var postToReturn = _mapper.Map<PostForDetailedDto>(post);

            return Ok(postToReturn);
        }

        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(PostForAddDto postForAddDto)
        {
            var userFromRepo = await _repo.GetUser(postForAddDto.UserId);

            if (userFromRepo == null)
                return Unauthorized();

            if (postForAddDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var postToCreate = _mapper.Map<Post>(postForAddDto);

            postToCreate.User = userFromRepo;
            postToCreate.Topic = await _repo.GetTopic(postForAddDto.TopicId);

            _repo.Add(postToCreate);

            if(await _repo.SaveAll()) {
                var postToReturn = _mapper.Map<PostForDetailedDto>(postToCreate);
                return CreatedAtRoute("GetPost", new {id = postToCreate.Id}, postToReturn);
            }

            throw new Exception("Poszt vagy kérdés mentése nem sikerült");
        }

        [HttpPost("AddAnswer")]
        public async Task<IActionResult> AddAnswer(AnswerForAddDto answerForAddDto)
        {
            var userFromRepo = await _repo.GetUser(answerForAddDto.UserId);

            if (userFromRepo == null)
                return Unauthorized();

            if (answerForAddDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var answerToCreate = _mapper.Map<Answer>(answerForAddDto);

            answerToCreate.User = userFromRepo;

            _repo.Add(answerToCreate);
            
            if(await _repo.SaveAll()) {
                var postCreated = await _repo.GetPostByID(answerToCreate.PostId);
                var postToReturn = _mapper.Map<PostForDetailedDto>(postCreated);
                return CreatedAtRoute("GetPost", new {id = postToReturn.Id}, postToReturn);
            }

            throw new Exception("Válasz mentése nem sikerül");
        }
    }
}