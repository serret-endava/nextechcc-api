using Newtonsoft.Json;
using NextechApi.Core.Entities;
using NextechApi.Infrastructure.StoryRepository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextechApi.Infrastructure.IStoryRepository
{
	public class StoryRepository<Item>: IStoryRepository<Item>
	{
		private readonly string _apiUrl;
		private readonly RestClient _client;

		public StoryRepository() 
		{
			_apiUrl = "https://hacker-news.firebaseio.com/v0/";
			_client = new RestClient(_apiUrl);
		}

		public async Task<Item> GetItem(int id) 
		{
			try
			{
				var request = new RestRequest($"{_apiUrl}item/{id}.json");
				var result = await _client.ExecuteGetAsync(request);
				Item? item = JsonConvert.DeserializeObject<Item>($"{result.Content}");
				return item;
			} catch (Exception ex) {
				throw;
			}
		}

		public async Task<List<int>> GetAll()
		{
			try
			{
				List<int> stories = new();
				var request = new RestRequest($"{_apiUrl}newstories.json");
				var response = await _client.ExecuteGetAsync(request);
				stories = JsonConvert.DeserializeObject<List<int>>($"{response.Content}") ?? [];
				return stories;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
