
using SDCT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SDCT.Services
{
    public class StoriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StoriesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<int>> GetStoriesAsync(int qty)
        {
            var valuesUri = _configuration["ApiSettings:bestStoriesUri"];
            var response = await _httpClient.GetStringAsync(valuesUri);
            var lstStories = JsonConvert.DeserializeObject<List<int>>(response);
            var lstStoriesFiltered = lstStories.Take(qty).ToList();

            return lstStoriesFiltered;
        }

        public async Task<StoryDTO> getStoriesDetailAsync(int id)
        {
            var storyUri = _configuration["ApiSettings:storyUri"];
            var completeUri = $"{storyUri}/{id}.json";
            var response = await _httpClient.GetStringAsync(completeUri);
            var detailJson = JObject.Parse(response);

            var objStory = new StoryDTO
            {
                postedBy = detailJson["by"]?.ToString(),
                commentCount = detailJson["descendants"]?.ToObject<int>(),
                score = detailJson["score"]?.ToObject<int>(),
                time = DateTimeOffset.FromUnixTimeSeconds(detailJson["time"].ToObject<long>()).DateTime,
                title = detailJson["title"]?.ToString(),
                uri = detailJson["url"]?.ToString()
            };

            return objStory;
        }
    }
}
