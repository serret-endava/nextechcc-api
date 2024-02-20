using NextechApi.Application.Services;
using NextechApi.Core.Entities;
using NextechApi.Infrastructure.StoryRepository;
using Microsoft.Extensions.Caching.Memory;

namespace NextechApi.Application.StoryService
{
	public class StoryService : IStoryService
    {
        private readonly IStoryRepository<Item> _storyRepository;
        private readonly IMemoryCache _memoryCache;

        public StoryService(IStoryRepository<Item> storiesRepository, IMemoryCache memoryCache)
        {
            _storyRepository = storiesRepository;
            _memoryCache = memoryCache;

        }

        public async Task<List<Story>> GetStoryList()
        {
            try
            {
                return await GetCachedStories();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Story>> GetCachedStories()
        {
            var stories = _memoryCache.Get<List<Story>>("stories");
            if (stories != null)
            {
                return stories;
            }

            var expirationTime = DateTimeOffset.Now.AddSeconds(30);
			var storiesIds = await _storyRepository.GetAll();
			List<Story> newStories = new();
			var tasks = storiesIds.Select(async id =>
			{
				Item fullItem = await _storyRepository.GetItem(id);
				if (fullItem != null)
				{
					newStories.Add(new()
					{
						Id = id,
						Title = fullItem.Title,
						Url = fullItem.Url,
						Text = fullItem.Text
					});
				}
			});
			await Task.WhenAll(tasks);
            _memoryCache.Set("stories", newStories, expirationTime);
            return newStories;
		}

        

    }
}
