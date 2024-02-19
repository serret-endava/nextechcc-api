using NextechApi.Application.Services;
using NextechApi.Core.Entities;
using NextechApi.Infrastructure.StoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextechApi.Application.StoryService
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository<Item> _storyRepository;

        public StoryService(IStoryRepository<Item> storiesRepository)
        {
            _storyRepository = storiesRepository;
        }

        public async Task<List<Story>> GetStoryList()
        {
            try
            {
                var storiesIds = await _storyRepository.GetAll();
                List<Story> stories = new();
                var tasks = storiesIds.Select(async id =>
                {
                    Item fullItem = await _storyRepository.GetItem(id);
                    if (fullItem != null)
                    {
                        stories.Add(new()
                        {
                            Id = id,
                            Title = fullItem.Title,
                            Url = fullItem.Url,
                            Text = fullItem.Text
                        });
                    }
                });
                await Task.WhenAll(tasks);
                return stories;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

    }
}
