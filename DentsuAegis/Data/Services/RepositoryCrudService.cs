﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IRepositoryCrudService
    {
        IQueryable<SearchRequest> GetSearchRequests();
        Task<IEnumerable<RepositoryInfo>> GetSearchResults(int searchID);
        Task<SearchRequest> AddSearchRequest(string query, IEnumerable<RepositoryInfo> repositories);
        Task<SearchRequest> UpdateSearchRequest(SearchRequest searchRequest, IEnumerable<RepositoryInfo> repositories);
    }
    public class RepositoryCrudService : IRepositoryCrudService
    {
        private readonly DataContext _dataContext;
        public RepositoryCrudService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<SearchRequest> AddSearchRequest(string query, IEnumerable<RepositoryInfo> repositories)
        {
            var searchRequest = new SearchRequest()
            {
                ExecutionDate = DateTime.Now,
                SearchString = query
            };
            searchRequest.SearchRequestAndRepositories = repositories
                .Select(x => new SearchRequestAndRepository()
                {
                    Repository = x,
                    SearchRequest = searchRequest
                }).ToList();

            await _dataContext.SearchRequests.AddAsync(searchRequest);

            await _dataContext.SaveChangesAsync();

            return searchRequest;
        }

        public IQueryable<SearchRequest> GetSearchRequests() => _dataContext.SearchRequests
                .OrderByDescending(x => x.ExecutionDate);

        public async Task<IEnumerable<RepositoryInfo>> GetSearchResults(int searchID) =>
            await _dataContext.SearchRequestAndRepositories
                .Where(x => x.SearchRequest.ID == searchID)
                .Select(x => x.Repository)
                .ToListAsync();

        public async Task<SearchRequest> UpdateSearchRequest(SearchRequest searchRequest, IEnumerable<RepositoryInfo> repositories)
        {
            searchRequest.ExecutionDate = DateTime.Now;

            searchRequest.SearchRequestAndRepositories = repositories.Select(x => new SearchRequestAndRepository()
            {
                Repository = x,
                SearchRequest = searchRequest
            }).ToList();

            _dataContext.Update(searchRequest);

            await _dataContext.SaveChangesAsync();

            return searchRequest;
        }
    }
}
