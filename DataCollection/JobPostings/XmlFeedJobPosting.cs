using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollection.JobPostings
{
    /// <summary>
    /// Follows the format from https://developer.indeed.com/docs/indeed-apply/enterprise-ats/
    /// </summary>
    public class XmlFeedJobPosting : IJobPosting
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ReferenceNumber { get; set; }
        public string RequisitionId { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string SourceName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public string Education { get; set; }
        public string JobType { get; set; }
        public string Category { get; set; }
        public string Experience { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string RemoteType { get; set; }
    }
}
