using DataScience.JobSearch;
using DataScience.JobSites;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataScience
{
    public class WebSiteCollector
    {
        public async Task<string> Collect(JobSiteInfo siteInfo, JobSearchParameters parameters)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(siteInfo.CreateHttpString(parameters));
            }

            return String.Empty;
        }
    }
}
