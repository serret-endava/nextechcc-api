using Microsoft.AspNetCore.Mvc;
using NextechApi.Application.Services;
using NextechApi.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NextechApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class StoriesController : ControllerBase
	{
		private readonly IStoryService _storyService;

		public StoriesController(IStoryService storyService)
		{
			_storyService = storyService;
		}

		// GET: api/<StoriesController>
		[HttpGet(Name = "stories")]
		public Task<List<Story>> Get()
		{
			return _storyService.GetStoryList();
		}


	}
}
