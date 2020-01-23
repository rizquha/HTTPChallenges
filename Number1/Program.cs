using System;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Fetcher;


namespace FetcherNumber1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Fetchers fetcher = new Fetchers();
            Console.WriteLine("=======================");
            Console.WriteLine("Get : \n"+fetcher.Get("https://httpbin.org/get"));
            Console.WriteLine("=======================");
            Console.WriteLine("Delete : \n"+fetcher.Delete("https://httpbin.org/delete"));
            Console.WriteLine("=======================");
            var jsonData = @"{
                        ""id"": 30,
                        ""name"": ""Someone""
                        }";
            Console.WriteLine("Post : \n"+fetcher.Post("https://httpbin.org/post",jsonData));
            Console.WriteLine("=======================");
            Console.WriteLine("Put : \n"+fetcher.Put("https://httpbin.org/put",jsonData));
            Console.WriteLine("=======================");
            Console.WriteLine("Patch : \n"+fetcher.Patch("https://httpbin.org/patch",jsonData));
            Console.WriteLine("=======================");

        }
    }
}
