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
            XmlFeedJobPostings jobs;
            if (args.Any())
            {
                jobs = ParseJobs(System.IO.File.ReadAllText(args[0]));
            }
            else
            {
                var jobFeed = new JobXmlFeed("https://www.workable.com/boards/workable.xml");
                jobs = ParseJobs(await jobFeed.GetXmlAsync());
            }

            var dataScienceJobs = jobs.Jobs.Where(x => x.Title.Contains("Data Sci", StringComparison.OrdinalIgnoreCase)).ToList();

            var xmlTagsRemover = new XmlTagsRemover();

            var descriptions = dataScienceJobs
                .Select(x => x.Description)
                .Select(x => xmlTagsRemover.RemoveTags(x));

            var wordCounter = new WordCounter.WordCounter();
            var giantDescription = string.Join(" ", descriptions);
            var results = wordCounter.Count(giantDescription);

            var sortedResults = results.OrderByDescending(x => x.Value).ToList();
            

        }

        private static XmlFeedJobPostings ParseJobs(string jobsXml)
        {
            var jobsSerializer = new XmlSerializer(typeof(XmlFeedJobPostings));

            using (var reader = XDocument.Parse(jobsXml).CreateReader())
                return (XmlFeedJobPostings)jobsSerializer.Deserialize(reader);
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
