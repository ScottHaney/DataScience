using DataScience.JobSearch;
using DataScience.JobSites;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;

namespace DataScience
{
    public class WebSiteCollector
    {
        public async Task<string> Collect(JobSiteInfo siteInfo, JobSearchParameters parameters)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(siteInfo.CreateHttpString(parameters));
                var content = await response.Content.ReadAsStringAsync();

                var xDoc = XDocument.Parse(content);
                var jobPostingLinks = xDoc.Root.Descendants("a")
                    .Where(x => x.Attribute("class")?.Value.Contains("job-listing__link") == true)
                    .Select(x => x.Attribute("href")?.Value)
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();
            }

            return String.Empty;
        }
    }
}
