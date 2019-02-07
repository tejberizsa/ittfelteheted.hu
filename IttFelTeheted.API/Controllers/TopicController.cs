using System.Threading.Tasks;
using IttFelTeheted.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace IttFelTeheted.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        public TopicController(IApplicationRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            var topics = await _repo.GetTopics();

            return Ok(topics);
        }

    }
}