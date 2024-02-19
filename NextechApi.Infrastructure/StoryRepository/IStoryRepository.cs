using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextechApi.Core.Entities;

namespace NextechApi.Infrastructure.StoryRepository
{
	public interface IStoryRepository<Item>
	{
		Task<Item> GetItem(int id);

		Task<List<int>> GetAll();
	}
}
