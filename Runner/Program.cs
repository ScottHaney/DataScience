using DataScience;
using DataScience.JobSearch;
using DataScience.JobSites;
using DataScience.JobXmlFeeds;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using DataCollection.JobPostings;
using System.Text.RegularExpressions;

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


            var regex1 = new Regex(@"<[\d\w_\-\.]+>");
            var regex2 = new Regex(@"</[\d\w_\-\.]+>");
            var regex3 = new Regex(@"<[\d\w_\-\.]+/>");

            var original = result.JobListings.First().Description;
            var filtered = regex1.Replace(original, "");
            filtered = regex2.Replace(filtered, "");
            filtered = regex3.Replace(filtered, "");



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
