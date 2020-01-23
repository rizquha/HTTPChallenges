using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;
using System.Collections.Generic;


namespace KompasNumber5
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            var html = "https://www.kompas.com/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            Console.WriteLine("====================Headlines Kompas.com Pada Tanggal 23 Januari 2020====================");

            var headlines = new List<List<string>>();
            var node = htmlDoc.DocumentNode.SelectNodes("//a[@class='headline__thumb__link']");
            foreach(var item in node)
            {
                var title = item.InnerText;
                var url = item.GetAttributeValue("href",string.Empty);
                headlines.Add(new List<string>{title,url});
            }
            Console.WriteLine("=========================================================================================");
            foreach(var item in headlines)
            {
                Console.WriteLine("Title : "+item[0]);
                Console.WriteLine("URL : ");
                Console.WriteLine("       "+item[1]);
                Console.WriteLine("=========================================================================================");
            }				
            
        }
    }
}
