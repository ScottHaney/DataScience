using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace DataCollection.DataFilters
{
    public class ParenthesizedTextExtractor
    {
        private readonly Regex _paranthesizedTextRegex = new Regex(@"\((.*)\)", RegexOptions.Compiled);

        public IEnumerable<string> GetTermPieces(string term)
        {
            var remainingText = term;

            var matches = _paranthesizedTextRegex.Matches(term);
            foreach (var match in matches.OfType<Match>().OrderByDescending(x => x.Index))
            {
                remainingText = remainingText.Remove(match.Index, match.Length);
                yield return match.Groups[1].Value.Trim();
            }

            yield return remainingText.Trim();
        }
    }
}
