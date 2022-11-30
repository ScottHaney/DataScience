using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.JobPostings.XmlFeed
{
    public class JobXmlFeed
    {
        private readonly string _url;

        public JobXmlFeed(string url)
        {
            _url = url;
        }

        public async Task<string> GetXmlAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_url);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
