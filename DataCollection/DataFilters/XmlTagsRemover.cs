using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataCollection.DataFilters
{
    public class XmlTagsRemover
    {
        private Regex regex1 = new Regex(@"<[\d\w_\-\.]+>");
        private Regex regex2 = new Regex(@"</[\d\w_\-\.]+>");
        private Regex regex3 = new Regex(@"<[\d\w_\-\.]+/>");

        public string RemoveTags(string text)
        {
            var filtered = regex1.Replace(text, "");
            filtered = regex2.Replace(filtered, "");
            filtered = regex3.Replace(filtered, "");

            return filtered;
        }
    }
}
