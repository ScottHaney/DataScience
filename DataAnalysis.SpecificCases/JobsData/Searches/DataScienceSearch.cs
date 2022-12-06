using DataCollection.DataFilters;
using DataCollection.JobPostings.XmlFeed;
using System;
using System.Linq;
using WordCounting;
using WordCounting.Counting;
using WordCounting.CharacterIdentification;
using DataCollection.ProgrammingLanguagesInfo;
using System.Threading.Tasks;
using DataCollection.KnownTerminology;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DataAnalysis.SpecificCases.JobsData.Searches
{
    public class DataScienceSearch
    {
        public async Task Run(XmlFeedJobPostings jobs)
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

            var programmingLanguageNames = await new WikipediaProgrammingLanguageNamesExtractor().GetLanguageNamesAsync();

            var programmingLanguageFrequencies = programmingLanguageNames
                .ToDictionary(x => x, x => overallResults.ContainsKey(x) ? overallResults[x] : 0)
                .OrderByDescending(x => x.Value)
                .ToList();

            var machineLearningTerms = await new MachineLearningGlossary().GetTermsAsync();

            var extractor = new ParenthesizedTextExtractor();
            var singleWordMachineLearningTerms = machineLearningTerms
                .SelectMany(x => extractor.GetTermPieces(x))
                .Where(x => x.All(x => !char.IsWhiteSpace(x)))
                .Distinct()
                .ToList();

            var machineLearningTermsFrequencies = singleWordMachineLearningTerms
                .ToDictionary(x => x, x => overallResults.ContainsKey(x) ? overallResults[x] : 0)
                .OrderByDescending(x => x.Value)
                .ToList();

            var sortedResults = overallResults.OrderByDescending(x => x.Value).ToList();

            var jobMatches = individualResults
                .Where(x => x.Counts.ContainsKey("k-means"))
                .Select(x => x.Description)
                .ToList();
        }
    }
}
