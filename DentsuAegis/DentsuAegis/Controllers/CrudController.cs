using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Services;
using DentsuAegis.Extensions;
using GitHubClientLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentsuAegis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudController : ControllerBase
    {
        private readonly IGitHubClient _gHubClient;
        private readonly IRepositoryCrudService _repositoryCrudService;

        public CrudController(IGitHubClient gHubClient, IRepositoryCrudService repositoryCrudService)
        {
            _gHubClient = gHubClient;
            _repositoryCrudService = repositoryCrudService;
        }

        [HttpGet]
        [Route("getSearches")]
        public async Task<IActionResult> GetSearches()
        {
            var recent = await _repositoryCrudService.GetSearchRequests()
                .Select(x => new
                {
                    x.ID,
                    x.SearchString
                }).ToListAsync();

            return Ok(recent);
        }

        [HttpGet]
        [Route("getReposForSearch/{searchID}")]
        public async Task<IActionResult> GetReposForSearch([FromRoute] int searchID)
        {
            var repositories = await _repositoryCrudService.GetSearchRequests()
                .Where(x => x.ID == searchID)
                .SelectMany(x => x.Repositories)
                .ToListAsync();

            return Ok(repositories);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string query)
        {
            var repositories = (await _gHubClient.SearchAsync(query)).ToEntity();

            var existingSearch = await _repositoryCrudService.GetSearchRequests()
                .Include(x => x.Repositories)
                .Where(x => x.SearchString.Equals(query))
                .SingleOrDefaultAsync();

            SearchRequest result = null;

            if(existingSearch != null)
                result = await _repositoryCrudService.UpdateSearchRequest(existingSearch, repositories);
            else
                result = await _repositoryCrudService.AddSearchRequest(query, repositories);

            return Ok(result);
        }

        [HttpGet]
        [Route("refreshSearch/{searchID}")]
        public async Task<IActionResult> RefreshSearch([FromRoute]int searchID)
        {
            var searchRequest = await _repositoryCrudService.GetSearchRequests()
                .Include(x => x.Repositories)
                .Where(x => x.ID == searchID)
                .SingleOrDefaultAsync();

            if (searchRequest?.SearchString == null)
                return NotFound();

            var repositories = (await _gHubClient.SearchAsync(searchRequest.SearchString)).ToEntity();

            var result = await _repositoryCrudService.UpdateSearchRequest(searchRequest, repositories);

            return Ok(result);
        }
    }
}
