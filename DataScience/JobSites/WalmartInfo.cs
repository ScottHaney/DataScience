using DataScience.JobSearch;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience.JobSites
{
    public class WalmartInfo : JobSiteInfo
    {
        private readonly string _url = "https://careers.walmart.com";

        public override string CreateHttpString(JobSearchParameters parameters)
        {
            return QueryHelpers.AddQueryString(_url, CreateParams(parameters));
        }

        private Dictionary<string, string> CreateParams(JobSearchParameters parameters)
        {
            var result = new Dictionary<string, string>();
            result["q"] = parameters.SearchQuery;

            if (parameters.Location != null)
            {
                if (!string.IsNullOrEmpty(parameters.Location.City))
                    result["jobCity"] = parameters.Location.City;
                if (!string.IsNullOrEmpty(parameters.Location.State))
                    result["jobState"] = parameters.Location.State;
            }

            return result;
        }
    }
}
