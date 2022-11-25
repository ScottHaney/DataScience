using DataScience.JobSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataScience.JobSites
{
    public abstract class JobSiteInfo
    {
        public abstract string CreateHttpString(JobSearchParameters parameters);
    }
}
