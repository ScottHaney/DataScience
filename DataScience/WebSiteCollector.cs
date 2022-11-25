using DataScience.JobSearch;
using DataScience.JobSites;
using System;

namespace DataScience
{
    public class WebSiteCollector
    {
        public string Collect(JobSiteInfo siteInfo, JobSearchParameters parameters)
        {
            return siteInfo.CreateHttpString(parameters);
        }
    }
}
