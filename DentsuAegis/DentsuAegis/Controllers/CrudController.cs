using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using GitHubClientLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentsuAegis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudController : ControllerBase
    {
        private readonly IGitHubClient _gHubClient;
        private readonly DataContext _dataContext;

        public CrudController(IGitHubClient gHubClient, DataContext dataContext)
        {
            _gHubClient = gHubClient;
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("getSearches")]
        public async Task<IActionResult> GetSearches()
        {
            var recent = await _dataContext.SearchRequests
                .OrderByDescending(x => x.ExecutionDate)
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
            var repositories = await _dataContext.SearchRequestAndRepositories
                .Where(x => x.SearchRequest.ID == searchID)
                .Select(x => x.Repository)
                .ToListAsync();

            return Ok(repositories);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string query)
        {
            var result = await _gHubClient.SearchAsync(query);
            var searchRequest = new SearchRequest()
            {
                ExecutionDate = DateTime.Now,
                SearchString = query
            };

            var repos = result.Items.Select(x => new RepositoryInfo()
            {
                AuthorAvatar = x.Owner.Avatar,
                AuthorLogin = x.Owner.Login,
                CodeLanguage = x.CodeLanguage,
                Description = x.Description,
                Forks = x.Forks,
                LastUpdate = x.LastUpdated,
                Link = x.Link,
                Stars = x.Stars,
                Title = x.Title,
            });
            searchRequest.SearchRequestAndRepositories = repos.Select(x => new SearchRequestAndRepository()
            {
                Repository = x,
                SearchRequest = searchRequest
            }).ToList();


            await _dataContext.SearchRequests.AddAsync(searchRequest);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("refreshSearch/{searchID}")]
        public async Task<IActionResult> RefreshSearch([FromRoute]int searchID)
        {
            var searchRequest = await _dataContext.SearchRequests
                .Include(x => x.SearchRequestAndRepositories)
                .Where(x => x.ID == searchID)
                .SingleOrDefaultAsync();

            if (searchRequest?.SearchString == null)
                return NotFound();

            var result = await _gHubClient.SearchAsync(searchRequest.SearchString);

            searchRequest.ExecutionDate = DateTime.Now;
            var repos = result.Items.Select(x => new RepositoryInfo()
            {
                AuthorAvatar = x.Owner.Avatar,
                AuthorLogin = x.Owner.Login,
                CodeLanguage = x.CodeLanguage,
                Description = x.Description,
                Forks = x.Forks,
                LastUpdate = x.LastUpdated,
                Link = x.Link,
                Stars = x.Stars,
                Title = x.Title,
            });
            searchRequest.SearchRequestAndRepositories = repos.Select(x => new SearchRequestAndRepository()
            {
                Repository = x,
                SearchRequest = searchRequest
            }).ToList();

            _dataContext.Update(searchRequest);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
