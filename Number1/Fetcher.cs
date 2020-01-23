using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Text;

namespace Fetcher
{
    public class Fetchers
    {
        private async Task<string> RequestObject(string url,HttpMethod methode, string content="")
        {
            HttpClient client = new HttpClient();
            var strContent = new StringContent(content,UnicodeEncoding.UTF8,"application/json");
            HttpRequestMessage requestMessage = new HttpRequestMessage(methode,url);
            requestMessage.Content = strContent;
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var response=await responseMessage.Content.ReadAsStringAsync();
            return response;
        }
        public string Get(string url)
        {
            var get= RequestObject(url,HttpMethod.Get).Result;
            return get;
        }
        public string Delete(string url)
        {
            var del= RequestObject(url,HttpMethod.Delete).Result;
            return del;
        }
        public string Post(string url,string content)
        {
            var post= RequestObject(url,HttpMethod.Post,content).Result;
            return post;
        }
        public string Put(string url,string content)
        {
            var put= RequestObject(url,HttpMethod.Put,content).Result;
            return put;
        }
        public string Patch(string url,string content)
        {
            var patch= RequestObject(url,HttpMethod.Patch,content).Result;
            return patch;
        }
    }
}
