using DataCollection.DataFilters;
using DataCollection.JobPostings.XmlFeed;
using System;
using System.Linq;
using WordCounting;
using WordCounting.Counting;
using WordCounting.CharacterIdentification;

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
                .Select(x => xmlTagsRemover.RemoveTags(x))
                .ToArray();

            var wordCounter = new WordCounter(wordCountMethod: new IsPresentWordCountMethod(), mergeCounts: true);
            var overallResults = wordCounter.Count(descriptions);

            var individualResults = descriptions
                .Select(x => new { Description = x, Counts = wordCounter.Count(x) })
                .ToList();

            var cSharpMatches = individualResults.Where(x => x.Counts.ContainsKey("hadoop"))
                .Select(x => x.Description)
                .ToList();

            var sortedResults = overallResults.OrderByDescending(x => x.Value).ToList();
        }
    }
}
