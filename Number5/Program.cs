using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;


namespace KompasNumber5
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,"https://www.kompas.com/");
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var response=await responseMessage.Content.ReadAsStringAsync();

            var html = "https://www.kompas.com/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var nodes =htmlDoc.DocumentNode.CssSelect(".nav__item a[href='https://news.detik.com/?tag_from=wp_firstnav_detikNews']");						
            foreach(var node in nodes)
            {
                Console.WriteLine(node.InnerHtml);
            }
            
        }
    }
}
