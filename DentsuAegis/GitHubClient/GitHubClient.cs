using GitHubClientLib.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubClientLib
{
    public interface IGitHubClient
    {
        Task<SearchResponseModel> SearchAsync(string query);
    }
    public class GitHubClient : IGitHubClient
    {
        private readonly GitHubClientOptions _options;
        private readonly IHttpClientFactory _clientFactory;
        private const string repo_search_path = "/search/repositories";
        public GitHubClient(IOptions<GitHubClientOptions> options,
            IHttpClientFactory clientFactory)
        {
            if (options?.Value == null)
                throw new ArgumentException("Ensure GitHubClientOptions is defined in appsettings");

            _clientFactory = clientFactory;
            _options = options.Value;
        }
        public async Task<SearchResponseModel> SearchAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Query cannot be empty");

            using (var _client = _clientFactory.CreateClient())
            {
                _client.DefaultRequestHeaders.Add("User-Agent", "DentsuAegis");

                var result = await _client.GetStringAsync(GetRepoSearchUrl(query));

                if (!string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<SearchResponseModel>(result);

                return null;

            }
        }
        private string GetRepoSearchUrl(string searchQuery) => 
            $"{_options.BaseUrl.TrimEnd('/')}{repo_search_path}?q={searchQuery}&sort=updated&per_page=10";
    }
}
