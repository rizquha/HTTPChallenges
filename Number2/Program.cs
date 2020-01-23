using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;

namespace EmployeesNumber2
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,"https://mul14.github.io/data/employees.json");
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var response=await responseMessage.Content.ReadAsStringAsync();
            
            var obj = JsonConvert.DeserializeObject<List<Employees>>(response);

            Console.WriteLine("1. Employees which have salary more than Rp15.000.000 : ");
            var a = from item in obj
                    where item.Salary>15000000
                    select new {first = item.First_Name, last=item.Last_Name};
            foreach(var i in a)
            {
                Console.WriteLine("* "+i.first+" "+i.last);
            }

            Console.WriteLine("\n");
            Console.WriteLine("2. Employees which life in Jakarta : ");
            var b = from item in obj
                    from itemX in item.Addresses
                    where itemX.City.Contains("Jakarta")
                    select new{first = item.First_Name, last = item.Last_Name};
            var distinct = b.Distinct();
            foreach(var i in distinct)
            {
                Console.WriteLine("* "+i.first+" "+i.last);
            }

            Console.WriteLine("\n");
            Console.WriteLine("3. Employees which birthday on March : ");
            var c = from item in obj    
                    where item.Birthday.Month==3
                    select new {first = item.First_Name, last = item.Last_Name};
            foreach(var i in c)
            {
                Console.WriteLine("* "+i.first+" "+i.last);
            }
            
            Console.WriteLine("\n");
            Console.WriteLine("4. Employees which birthday on March : ");
            var d = from item in obj
                    where item.Departmen.Name=="Research and development"
                    select new {first=item.First_Name,last=item.Last_Name};
            foreach(var i in d)
            {
                Console.WriteLine("* "+i.first+" "+i.last);
            }

            Console.WriteLine("\n");
            Console.WriteLine("5. How many each employee absences in October 2019");
            var e = from item in obj    
                    from itemX in item.Presence_List
                    where itemX.Month==10 && itemX.Year==2019
                    select $"{item.First_Name} {item.Last_Name}";
            Dictionary<string,int> dict = e.GroupBy(a => a).ToDictionary
            (
                x => x.Key,
                x => x.Count()

            );
            foreach(var i in dict)
            {
                Console.WriteLine("* "+i.Key+" = "+i.Value);
            }
        }
    }
    class Employees
    {
        [JsonProperty("id")]
        public int Id{get;set;}
        [JsonProperty("avatar_url")]
        public string Avatar_Url{get;set;}
        [JsonProperty("employee_id")]
        public string Employee_Id{get;set;}
        [JsonProperty("first_name")]
        public string First_Name{get;set;}
        [JsonProperty("last_name")]
        public string Last_Name{get;set;}
        [JsonProperty("email")]
        public string Email{get;set;}
        [JsonProperty("username")]
        public string Username{get;set;}
        [JsonProperty("birthday")]
        public DateTime Birthday{get;set;}


        [JsonProperty("addresses")]
        public List<Addresses> Addresses{get;set;} = new List<Addresses>();
        [JsonProperty("phones")]
        public List<Phones> Phones {get;set;} = new List<Phones>();
        

        [JsonProperty("presence_list")]
        public List<DateTime> Presence_List{get;set;} = new List<DateTime>();


        [JsonProperty("salary")]
        public int Salary {get;set;}


        [JsonProperty("department")]
        public Departmen Departmen {get;set;}
        [JsonProperty("position")]
        public Position Position {get;set;}
    }

    class Addresses
    {
        [JsonProperty("label")]
        public string Label {get;set;}
        [JsonProperty("address")]
        public string Address {get;set;}
        [JsonProperty("city")]
        public string City {get;set;}
    }
    class Phones
    {
        [JsonProperty("label")]
        public string Label {get;set;}
        [JsonProperty("phone")]
        public string Phone {get;set;}
    }
    class Departmen
    {
        [JsonProperty("name")]
        public string Name{get;set;}

    }
    class Position
    {
        [JsonProperty("name")]
        public string Name{get;set;}

    }
}
