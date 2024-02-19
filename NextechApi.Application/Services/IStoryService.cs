using NextechApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextechApi.Application.Services
{
    public interface IStoryService
    {
        Task<List<Story>> GetStoryList();
    }
}
