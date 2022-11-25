using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataCollection.JobPostings
{
    /// <summary>
    /// Follows the format from https://developer.indeed.com/docs/indeed-apply/enterprise-ats/
    /// </summary>
    [XmlRoot(ElementName = "job")]
    public class XmlFeedJobPosting : IJobPosting
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "referencenumber")]
        public string ReferenceNumber { get; set; }
        [XmlElement(ElementName = "requisitionid")]
        public string RequisitionId { get; set; }
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "company")]
        public string Company { get; set; }
        [XmlElement(ElementName = "sourcename")]
        public string SourceName { get; set; }
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "postalcode")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "streetaddress")]
        public string StreetAddress { get; set; }
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "salary")]
        public string Salary { get; set; }
        [XmlElement(ElementName = "education")]
        public string Education { get; set; }
        [XmlElement(ElementName = "jobtype")]
        public string JobType { get; set; }
        [XmlElement(ElementName = "category")]
        public string Category { get; set; }
        [XmlElement(ElementName = "experience")]
        public string Experience { get; set; }
        [XmlElement(ElementName = "expirationdate")]
        public string ExpirationDate { get; set; }
        [XmlElement(ElementName = "remotetype")]
        public string RemoteType { get; set; }
    }
}
