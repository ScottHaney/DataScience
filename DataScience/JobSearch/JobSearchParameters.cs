using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience.JobSearch
{
    public class JobSearchParameters
    {
        public string SearchQuery { get; set; }
        public JobLocation Location { get; set; }
    }

    public class JobLocation
    {
        public string City { get; set; }
        public string State { get; set; }
    }
}
