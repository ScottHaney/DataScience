using DataScience;
using DataScience.JobSearch;
using DataScience.JobSites;
using DataScience.JobXmlFeeds;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Runner
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var jobFeed = new JobXmlFeed("https://www.workable.com/boards/workable.xml");
            var jobs = await jobFeed.GetXmlAsync();

            var jobsXml = XDocument.Parse(jobs);

            var publisher = jobsXml.Root.Descendants("publisher").FirstOrDefault();
            var publisherUrl = jobsXml.Root.Descendants("publisherurl").FirstOrDefault();
            var job = jobsXml.Root.Descendants("job").ToList();

            var result = new Jobs(publisher?.Value,
                publisherUrl?.Value,
                job?.Select(x => x.Value).ToList() ?? new List<string>());


            var collector = new WebSiteCollector();

            var parameters = new JobSearchParameters()
            {
                SearchQuery = "Data Science"
            };

            //var result = await collector.Collect(new WalmartInfo(), parameters);
        }
    }

    public class Jobs
    {
        public readonly string Publisher;
        public readonly string PublisherUrl;
        public readonly List<string> JobListings;

        public Jobs(string publisher,
            string publisherUrl,
            List<string> jobListings)
        {
            Publisher = publisher;
            PublisherUrl = publisherUrl;
            JobListings = jobListings;
        }
    }
}
