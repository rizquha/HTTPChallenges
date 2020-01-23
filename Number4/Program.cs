using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace MovieDB
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string IndonesianMoviesURL = "https://api.themoviedb.org/3/search/movie?api_key=56b30ff15cc32100022a429ef12415c9&language=id&query=Indonesia&page=1&include_adult=true&region=id-ID";
            Console.WriteLine("1. Get 10+ titles of Indonesian movies : ");
            await MoviesList(IndonesianMoviesURL,"original_title");

            string KeanuMoviesURL = "https://api.themoviedb.org/3/discover/movie?api_key=56b30ff15cc32100022a429ef12415c9&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_cast=6384";
            Console.WriteLine("2. Get movie list played by Keanu Reeves : ");
            await MoviesList(KeanuMoviesURL,"original_title");

            string RobertTomMoviesURL = "https://api.themoviedb.org/3/discover/movie?api_key=56b30ff15cc32100022a429ef12415c9&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_cast=1136406%2C3223";
            Console.WriteLine("3. Get movie list played by Robert Downey Jr. and Tom Holland : ");
            await MoviesList(RobertTomMoviesURL,"original_title");

            string ReleaseVotesURL ="https://api.themoviedb.org/3/discover/movie?api_key=56b30ff15cc32100022a429ef12415c9&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&primary_release_year=2016&vote_average.gte=7.5";
            Console.WriteLine("4. Get popular movie list that released on 2016 and the votes above 7.5 : ");
            await MoviesList(ReleaseVotesURL,"title");
               
        }
        public static async Task MoviesList(string url,string detected)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,url);
            
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var response = await responseMessage.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<Object>(response);
            var movies = from item in obj.results
                         select item.original_title;
            if(detected=="original_title")
            {
                foreach(var item in movies)
                {
                    Console.WriteLine("* "+item);    
                }
                Console.WriteLine("\n");
            }
            else if(detected=="title")
            {
                var movie = from item in obj.results
                            select item.title;
                foreach(var item in movie)
                {
                    Console.WriteLine("* "+item);    
                }
                Console.WriteLine("\n");
            }  
        }
    }
    class Results
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }  

    }
    class Object
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Results> results { get; set; }
    }
}
