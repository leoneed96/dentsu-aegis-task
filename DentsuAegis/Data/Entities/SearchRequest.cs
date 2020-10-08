using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class SearchRequest
    {
        public int ID { get; set; }
        public string SearchString { get; set; }
        public DateTime ExecutionDate { get; set; }
        public ICollection<RepositoryInfo> Repositories{ get; set; }
    }
}
