using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IttFelTeheted.API.Data;
using IttFelTeheted.API.Dtos;
using IttFelTeheted.API.Models;
using Microsoft.AspNetCore.Authorization;
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

            if (User.Identity.IsAuthenticated)
            foreach (var post in postsToReturn)
            {
                post.IsFollowedByCurrentUser = posts.First(p => p.Id == post.Id)
                                                    .PostFollower.Any(f => f.FollowerId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

            return Ok(postsToReturn);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _repo.GetPostByID(id, true);

            var postToReturn = _mapper.Map<PostForDetailedDto>(post);

            if (User.Identity.IsAuthenticated)
                postToReturn.IsFollowedByCurrentUser = post.PostFollower.Any(f => f.FollowerId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            return Ok(postToReturn);
        }

        [HttpGet("answer/{id}", Name = "GetAnswer")]
        public async Task<IActionResult> GetAnswer(int id)
        {
            var answer = await _repo.GetAnswerByID(id);

            var answerToReturn = _mapper.Map<AnswerForDetailedDto>(answer);

            return Ok(answerToReturn);
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

        [HttpPost("{postId}/AddAnswer")]
        [Authorize]
        public async Task<IActionResult> AddAnswer(int postId, AnswerForAddDto answerForAddDto)
        {
            var userFromRepo = await _repo.GetUser(answerForAddDto.UserId);

            if (userFromRepo == null)
                return Unauthorized();

            if (answerForAddDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var answerToCreate = _mapper.Map<Answer>(answerForAddDto);

            answerToCreate.User = userFromRepo;

            var postFromRepo = await _repo.GetPostByID(postId);
            if (postFromRepo == null)
                return BadRequest("Posz vagy válasz nem elérhető");

            answerToCreate.Post = postFromRepo;

            _repo.Add(answerToCreate);
            
            if(await _repo.SaveAll()) {
                var answerToReturn = _mapper.Map<AnswerForDetailedDto>(answerToCreate);
                return CreatedAtRoute("GetAnswer", new {id = answerToReturn.Id}, answerToReturn);
            }

            throw new Exception("Válasz mentése nem sikerül");
        }

        [HttpGet("trending/{id}")]
        public async Task<IActionResult> GetTrendingPosts(int id)
        {
            var baseTopic = await _repo.GetTopic(id);

            if (baseTopic == null)
                return BadRequest("Poszt nem elérhető");

            var trendingPosts = await _repo.GetTrendingPosts(id, baseTopic.Id);

            var result = _mapper.Map<IEnumerable<PostForTrendingDto>>(trendingPosts);

            return Ok(result);
        }

        [HttpPost("{userId}/like/{answerId}")]
        public async Task<IActionResult> LikeAnswer(int userId, int answerId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var vote = await _repo.GetVote(userId, answerId);

            if (vote != null)
                return BadRequest("Már szavaztál erre a válaszra!");

            var answer = await _repo.GetAnswerByID(answerId);
            if (answer == null)
                return NotFound();
            
            vote = new Vote
            {
                VoterId = userId,
                VotedId = answerId,
                IsCorrect = true
            };
            _repo.Add<Vote>(vote);

            answer.Like++;

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Szavazat nem sikerült");
        }

        [HttpPost("{userId}/dislike/{answerId}")]
        public async Task<IActionResult> DislikeAnswer(int userId, int answerId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var vote = await _repo.GetVote(userId, answerId);

            if (vote != null)
                return BadRequest("Már szavaztál erre a válaszra!");

            var answer = await _repo.GetAnswerByID(answerId);
            if (answer == null)
                return NotFound();
            
            vote = new Vote
            {
                VoterId = userId,
                VotedId = answerId,
                IsCorrect = false
            };
            _repo.Add<Vote>(vote);

            answer.Like--;

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Szavazat nem sikerült");
        }

        [HttpPost("{userId}/follow/{followedId}")]
        public async Task<IActionResult> FollowPost(int userId, int followedId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var postFollow = await _repo.GetPostFollow(userId, followedId);

            if (postFollow != null)
                return BadRequest("Már követed");

            var followed = await _repo.GetPostByID(followedId);
            if (followed == null)
                return NotFound();
            
            postFollow = new PostFollow
            {
                FollowerId = userId,
                FollowedId = followedId
            };
            _repo.Add<PostFollow>(postFollow);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Nem sikerült a feliratkozás");
        }

        [HttpPost("{userId}/unfollow/{followedId}")]
        public async Task<IActionResult> UnfollowPost(int userId, int followedId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var postFollow = await _repo.GetPostFollow(userId, followedId);

            if (postFollow == null)
                return BadRequest("Nem követed ezt a posztot");

            _repo.Delete<PostFollow>(postFollow);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Nem sikerült a leiratkozás");
        }
    }
}