using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataCollection.JobPostings.XmlFeed
{
    [XmlRoot("source")]
    public class XmlFeedJobPostings
    {
        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlElement("publisherurl")]
        public string PublisherUrl { get; set; }

        [XmlElement("job")]
        public List<XmlFeedJobPosting> Jobs { get; set; }
    }
}
