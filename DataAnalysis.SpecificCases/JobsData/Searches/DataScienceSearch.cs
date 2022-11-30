using DataCollection.DataFilters;
using DataCollection.JobPostings.XmlFeed;
using System;
using System.Linq;
using WordCounting;

namespace DataAnalysis.SpecificCases.JobsData.Searches
{
    public class DataScienceSearch
    {
        public void Run(XmlFeedJobPostings jobs)
        {
            var dataScienceJobs = jobs.Jobs.Where(x => x.Title.Contains("Data Sci", StringComparison.OrdinalIgnoreCase)).ToList();

            var xmlTagsRemover = new XmlTagsRemover();

            var descriptions = dataScienceJobs
                .Select(x => x.Description)
                .Select(x => xmlTagsRemover.RemoveTags(x));

            var wordCounter = new WordCounter();
            var giantDescription = string.Join(" ", descriptions);
            var results = wordCounter.Count(giantDescription);

            var sortedResults = results.OrderByDescending(x => x.Value).ToList();
        }
    }
}
