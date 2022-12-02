using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using HtmlAgilityPack;

namespace DataCollection.ProgrammingLanguagesInfo
{
    public class WikipediaProgrammingLanguageNamesExtractor
    {
        private static readonly string _url = "https://en.wikipedia.org/wiki/List_of_programming_languages";

        public async Task<List<string>> GetLanguageNamesAsync()
        {
            var web = new HtmlWeb();
            var doc = web.Load(_url);

            var languageNames = doc.DocumentNode
                .SelectNodes("//div[@class='div-col']/ul/li/a")
                .Select(x => x.InnerText)
                .ToList();

            return languageNames;
        }
    }
}