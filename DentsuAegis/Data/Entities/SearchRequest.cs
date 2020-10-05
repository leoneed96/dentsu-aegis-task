using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class SearchRequestAndRepository
    {
        public SearchRequest SearchRequest { get; set; }
        public int SearchRequestID { get; set; }

        public RepositoryInfo Repository { get; set; }
        public int RepositoryID { get; set; }
    }
}
