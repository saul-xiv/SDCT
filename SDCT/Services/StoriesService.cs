
using SDCT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace SDCT.Services
{
    public class StoriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public StoriesService(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<List<int>> GetStoriesAsync(int qty)
        {
            var cacheKey = "ListStories";
            if (!_cache.TryGetValue(cacheKey, out List<int> lstCacheada))
            {
                var valuesUri = _configuration["ApiSettings:bestStoriesUri"];
                var response = await _httpClient.GetStringAsync(valuesUri);
                lstCacheada = JsonConvert.DeserializeObject<List<int>>(response);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _cache.Set(cacheKey, lstCacheada, cacheEntryOptions);
            }

            return lstCacheada.Take(qty).ToList();
        }

        public async Task<StoryDTO> getStoriesDetailAsync(int id)
        {
            var cacheKey = $"StoryDetail_{id}";
            if (!_cache.TryGetValue(cacheKey, out StoryDTO storyDTO))
            {
                var storyUri = _configuration["ApiSettings:storyUri"];
                var completeUri = $"{storyUri}/{id}.json";
                var response = await _httpClient.GetStringAsync(completeUri);
                var detailJson = JObject.Parse(response);

                storyDTO = new StoryDTO
                {
                    postedBy = detailJson["by"]?.ToString(),
                    commentCount = detailJson["descendants"]?.ToObject<int>(),
                    score = detailJson["score"]?.ToObject<int>(),
                    time = DateTimeOffset.FromUnixTimeSeconds(detailJson["time"].ToObject<long>()).DateTime,
                    title = detailJson["title"]?.ToString(),
                    uri = detailJson["url"]?.ToString()
                };

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _cache.Set(cacheKey, storyDTO, cacheEntryOptions);
            }

            return storyDTO;
        }
    }
}
