using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using WebCoreMongo.Models;

namespace WebCoreMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private AppSettings AppSettings { get; set; }
        public IHostingEnvironment HostingEnvironment { get; private set; }

        public ValuesController(IConfiguration configuration, IOptions<AppSettings> settings, IHostingEnvironment env)
        {
            Configuration = configuration;
            AppSettings = settings.Value;
            HostingEnvironment = env;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var connectionString = Configuration.GetValue<string>("Mongo:connectionString");
            var connectionString = AppSettings.MongoConnectionString;

            var client = new MongoClient(connectionString);
            //var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("Mega");
            var dbs = client.ListDatabaseNames().ToList();
            var col = db.GetCollection<Person>("first");
            var c = col.CountDocuments(new BsonDocument());

            var names = col.Find(new BsonDocument()).ToList().Select(e => e.Name).ToList();

            return names;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

            string url = AppSettings.WebAppUrl + id;
            var baseUri = new Uri($"{this.Request.Scheme}://{this.Request.Host}");

            //try
            //{
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    client.BaseAddress = baseUri;
                    var res = client.GetAsync(url);
                    var d = res.Result.Content.ReadAsStringAsync();
                    return d.Result;
                }
            //}
            //catch(Exception ex)
            //{
            //    using (System.Net.WebClient client = new System.Net.WebClient())
            //    {

            //        // Add a user agent header in case the 
            //        // requested URI contains a query.

            //        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            //        Stream data = client.OpenRead(url);
            //        StreamReader reader = new StreamReader(data);
            //        string s = reader.ReadToEnd();
            //        Console.WriteLine(s);
            //        data.Close();
            //        reader.Close();
            //    }
            //}
            

            //return "error";


        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
