using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class RepositoryInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorLogin { get; set; }
        public string AuthorAvatar { get; set; }
        public string Link { get; set; }
        public string CodeLanguage { get; set; }
        public int Stars { get; set; }
        public int Forks { get; set; }
        public DateTime LastUpdate { get; set; }
        public int SearchId { get; set; }
        public SearchRequest Search { get; set; }
    }
}
