using DataScience;
using DataScience.JobSearch;
using DataScience.JobSites;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using DataCollection.JobPostings;
using System.Text.RegularExpressions;
using DataCollection.DataFilters;
using DataCollection.JobPostings.XmlFeed;

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

            var serializer = new XmlSerializer(typeof(XmlFeedJobPosting));


            var result = new Jobs(publisher?.Value,
                publisherUrl?.Value,
                job?.Select(x => (XmlFeedJobPosting)serializer.Deserialize(x.CreateReader())).ToList() ?? new List<XmlFeedJobPosting>());

            var xmlTagsRemover = new XmlTagsRemover();
            var filtered = xmlTagsRemover.RemoveTags(result.JobListings.First().Description);

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
        public readonly List<XmlFeedJobPosting> JobListings;

        public Jobs(string publisher,
            string publisherUrl,
            List<XmlFeedJobPosting> jobListings)
        {
            Publisher = publisher;
            PublisherUrl = publisherUrl;
            JobListings = jobListings;
        }
    }
}
