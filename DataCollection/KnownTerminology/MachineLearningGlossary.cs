using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataCollection.KnownTerminology
{
    public class MachineLearningGlossary
    {
        private readonly string _url = "https://developers.google.com/machine-learning/glossary/";

        public async Task<List<string>> GetTermsAsync()
        {
            var web = new HtmlWeb();
            var doc = web.Load(_url);
            doc.OptionEmptyCollection = true;

            var glossaryTerms = doc.DocumentNode
                .SelectNodes("//h2[@class='hide-from-toc']")
                .Select(x => x.InnerText.Trim())
                .ToList();

            return glossaryTerms;
        }
    }
}
