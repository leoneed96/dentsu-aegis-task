using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GitHubClientLib.Models
{
    public class SearchResponseModel
    {
        public List<SearchResponseItemModel> Items { get; set; }
    }
    public class SearchResponseItemModel
    {
        [JsonProperty("name")]
        public string Title { get; set; }
        public string Description { get; set; }
        public OwnerModel Owner { get; set; }
        public int Forks { get; set; }
        [JsonProperty("stargazers_count")]
        public int Stars { get; set; }
        [JsonProperty("html_url")]
        public string Link { get; set; }
        [JsonProperty("language")]
        public string CodeLanguage { get; set; }
        [JsonProperty("updated_at")]
        public DateTime LastUpdated { get; set; }
    }

    public class OwnerModel
    {
        public string Login { get; set; }
        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }
    }
}
