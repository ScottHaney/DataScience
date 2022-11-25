using DataScience.JobSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience.JobSites
{
    public class WalmartInfo : JobSiteInfo
    {
        private readonly string _url = "careers.walmart.com";

        public override string CreateHttpString(JobSearchParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
