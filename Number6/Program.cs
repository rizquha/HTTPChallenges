using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using System.Net.Http;
using PuppeteerSharp;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;

using System.Text;
using System.Collections.Generic;

namespace CGVNumber6
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("\n");
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            Console.WriteLine("=================================CGV NOW PLAYING==================================\n");

            var launch = new LaunchOptions 
            { 
               Headless = true 
            };
            using (var chrome = await Puppeteer.LaunchAsync(launch))
            using (var newPage = await chrome.NewPageAsync())
            {
                await newPage.GoToAsync("https://www.cgv.id/en/movies/now_playing");
                var selector = await newPage.QuerySelectorAllHandleAsync(".movie-list-body > ul >li > a").EvaluateFunctionAsync<string[]>("e => e.map(a => a.href)");
                HtmlWeb web = new HtmlWeb();



                for(int i=0;i<selector.Length;i++)
                {
                var htmlDoc = web.Load(selector[i]);

                var title =   htmlDoc.DocumentNode.SelectNodes("//div[@class='movie-info-title']");
                foreach(var item in title)
                {
                    Console.WriteLine("============================="+item.InnerText.Trim()+"=============================\n");
                }                

                var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='movie-add-info left']/ul/li");
                foreach(var item in nodes)
                {
                    Console.WriteLine("* "+item.InnerText.Trim());
                }

                var trailer = htmlDoc.DocumentNode.SelectNodes("//div[@class='trailer-btn-wrapper']/img");
                foreach(var item in trailer)
                {
                    var trailerMovie = item.InnerText;
                    var URL = item.GetAttributeValue("onclick",string.Empty);
                    string result = URL.Split(new string[] { "'" }, 3, StringSplitOptions.None)[1];
                    Console.WriteLine("* TRAILER : "+result);
                }

                var sinopsis = htmlDoc.DocumentNode.SelectNodes("//div[@class='movie-synopsis right']");
                Console.WriteLine("* SINOPSIS : ");
                foreach(var item in sinopsis)
                {
                    Console.WriteLine("             "+item.InnerText.Trim());
                    Console.WriteLine("\n");
                    Console.WriteLine("==========================================================================");
                }
                
                }
            }
        }
    }
}
