using Data.Entities;
using GitHubClientLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DentsuAegis.Extensions
{
    public static class RepositoryExtensions
    {
        public static IEnumerable<RepositoryInfo> ToEntity(this SearchResponseModel model) =>
            model?.Items?.Select(x => new RepositoryInfo()
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
    }
}
