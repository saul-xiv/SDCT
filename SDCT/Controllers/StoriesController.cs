using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SDCT.Models;
using SDCT.Services;

namespace SDCT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {

        private readonly StoriesService _storiesService;
        public StoriesController(StoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        [HttpGet("{qty}")]
        public async Task<IActionResult> getStories(int qty = 500)
        {
            var lstStoriesId = await _storiesService.GetStoriesAsync(qty);

            var lstStories = new List<StoryDTO>();

            foreach (int id in lstStoriesId)
            {
                StoryDTO story = await _storiesService.getStoriesDetailAsync(id);
                lstStories.Add(story);
            }

            lstStories.OrderByDescending(x => x.score);

            return Ok(lstStories);
        }

    }
}
