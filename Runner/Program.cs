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
using DataAnalysis.SpecificCases.JobsData.Searches;
using DataCollection.ProgrammingLanguagesInfo;

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

            await new DataScienceSearch().Run(jobs);
        }

        private static XmlFeedJobPostings ParseJobs(string jobsXml)
        {
            var jobsSerializer = new XmlSerializer(typeof(XmlFeedJobPostings));

            using (var reader = XDocument.Parse(jobsXml).CreateReader())
                return (XmlFeedJobPostings)jobsSerializer.Deserialize(reader);
        }
    }
}
