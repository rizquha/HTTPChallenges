using System;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;

namespace PostUserNumber3
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            static async Task<List<Posts>> getPosts()
            {
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
                return JsonConvert.DeserializeObject<List<Posts>>(result);
            }
            
            static async Task<List<Users>> getUsers()
            {
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync("https://jsonplaceholder.typicode.com/users");
                return JsonConvert.DeserializeObject<List<Users>>(result);
            }
            var getUser = getUsers().GetAwaiter().GetResult();
            var getPost = getPosts().GetAwaiter().GetResult();
            var combine = new List<UserPostCombine>();

            foreach (var post in getPost)
            {
                foreach (var user in getUser)
                {
                    if (post.UserId == user.Id)
                    {
                        UserPostCombine userpostcombine = new UserPostCombine();
                        userpostcombine.UserId = post.UserId;
                        userpostcombine.Id = post.Id;
                        userpostcombine.Title = post.Title;
                        userpostcombine.Body = post.Body;
                        userpostcombine.users = user;
                        combine.Add(userpostcombine);
                    }
                }
            }

            string combineJson = JsonConvert.SerializeObject(combine,Formatting.Indented);
            Console.WriteLine(combineJson);

        }
    }
    class Posts
    {
        [JsonProperty("userId")]
        public int UserId {get;set;}
        [JsonProperty("id")]
        public int Id {get;set;}
        [JsonProperty("title")]
        public string Title {get;set;}
        [JsonProperty("body")]
        public string Body {get;set;}   
    }
    class Users
    {
        [JsonProperty("id")]
        public int Id {get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
        [JsonProperty("username")]
        public string USername{get;set;}
        [JsonProperty("email")]
        public string Email{get;set;}
        [JsonProperty("address")]
        public Address Address {get;set;}
        [JsonProperty("phone")]
        public string Phone {get;set;}
        [JsonProperty("website")]
        public string Website{get;set;}
        [JsonProperty("company")]
        public Company Company {get;set;}

    }
    class Address
    {
        [JsonProperty("street")]
        public string Street {get;set;}
        [JsonProperty("suite")]
        public string Suite {get;set;}
        [JsonProperty("city")]
        public string City {get;set;}
        [JsonProperty("zipcode")]
        public string Zipcode{get;set;}
        [JsonProperty("geo")]
        public Geo Geo {get;set;}
        
    }
    class Geo
    {
        [JsonProperty("lat")]
        public double Latitude {get;set;}
        [JsonProperty("lng")]
        public double Longitude {get;set;}
    }
    class Company
    {
        [JsonProperty("name")]
        public string Name {get;set;}
        [JsonProperty("catchPhrase")]
        public string CatchPhrase {get;set;}
        [JsonProperty("bs")]
        public string Bs {get;set;}

    }
    class UserPostCombine
    {

        [JsonProperty("userId")]
        public int UserId {get;set;}
        [JsonProperty("id")]
        public int Id {get;set;}
        [JsonProperty("title")]
        public string Title {get;set;}
        [JsonProperty("body")]
        public string Body {get;set;} 
        [JsonProperty("user")]
        public Users users { get; set; }

    }

}
