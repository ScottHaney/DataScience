using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollection.DataFilters;
using NUnit.Framework;

namespace DataCollection.Tests
{
    public class ParenthesizedTextExtractorTests
    {
        [Test]
        public void Breaks_Up_Text_With_One_Unparenthesized_Term_And_One_Parenthesized_Term()
        {
            var extractor = new ParenthesizedTextExtractor();

            var text = "first (second)";
            var items = extractor.GetTermPieces(text).ToList();

            CollectionAssert.AreEquivalent(new[] { "first", "second" }, items);
        }
    }
}
