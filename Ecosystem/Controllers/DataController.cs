using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecosystem.RepoService;
using Ecosystem.Entity;
using Pagination;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace Ecosystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        private readonly IInsertTable<service> inserttable;
        private readonly IOTManager context;
        private readonly IConfiguration config;

        public DataController(IInsertTable<service> inserttable, IOTManager context, IConfiguration config)
        {
            this.inserttable = inserttable;
            this.context = context;
            this.config = config;
        }

        [HttpPost]
        [Route("createtable")]
        public IActionResult CreateTable(JsonElement value)
        {
            var table = JObject.Parse(value.ToString());
            try
            {
                inserttable.CreateTable(table["segreto"].ToString());
                return Ok("ok");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("setappsettings")]
        public IActionResult SetAppsettings(JsonElement model)
        {
            string path = @"c:\temp\MyTest.txt";
            string appsettingspath = @".\appsettings.json";
            var textsetting = System.IO.File.ReadAllText(appsettingspath);

            var jsonmodel = JObject.Parse(model.ToString());
            var jsonsetting = JObject.Parse(textsetting);
            jsonsetting["AppSettings"]["segreto"] = jsonmodel["segreto"];

            System.IO.File.WriteAllText(appsettingspath, jsonsetting.ToString());


            //var obj = JsonConvert.DeserializeObject<dynamic>(model.ToString());
            //using (var fileStream = new FileStream(@"C:\Project\IOTManager\src\app\Model\service.ts", FileMode.Open))
            //{

            //    using (var text = new StreamReader(fileStream, Encoding.UTF8))
            //    {
            //        var result = text.ReadToEnd();

            //    }
            //}
            return Ok("ok");
        }


        [HttpPost]
        [Route("savejsonfile")]
        public IActionResult SaveJsonFile(JsonElement model)
        {
            string appsettingspath = @"C:\projecttemp\Ecosystem\Ecosystem\ClientApp\src\app\Model\datamodel.json";
            //     var jsonsetting = JObject.Parse(model.ToString());
            System.IO.File.WriteAllText(appsettingspath, model.ToString());
            return Ok("OK");
        }


        [HttpPost]
        [Route("saveforeigntable")]
        public IActionResult SaveForeignTable(JsonElement model)
        {
            string appsettingspath = @".\appsettings.json";
            var config = System.IO.File.ReadAllText(appsettingspath);
            var jmodel = JsonConvert.DeserializeObject<dynamic>(model.ToString());
            JObject joconfig = JObject.Parse(config);
            JObject dbrelation =(JObject) joconfig["dbrelation"];
            //dbrelation.Property("service").AddAfterSelf(new JProperty("ciao", new List<string> { "io", "tu", "noi" }));
            //     JArray service = (JArray)dbrelation["service"];

            string tb = dbrelation.Properties().Select(k => k.Name).First();

            foreach (JProperty property in jmodel)
            {
                var t = property.Name;
                var t1 = property.Value;
                dbrelation.Property("sp").AddAfterSelf(new JProperty(t, t1));
            }
            //JObject o = new JObject
            //{
            //    { "name1", "value1" },
            //    { "name2", "value2" }
            //};
            //foreach (JProperty property in dbrelation.Properties())
            //{
            //    Console.WriteLine(property.Name + " - " + property.Value);
            //}

            //foreach (KeyValuePair<string, JToken> property in o)
            //{
            //    Console.WriteLine(property.Key + " - " + property.Value);
            //}

            var json = JsonConvert.SerializeObject(joconfig);
            System.IO.File.WriteAllText(appsettingspath, json);
            return Ok("OK");
        }

        [HttpPost]
        [Route("setkey")]
        public IActionResult SetKey(JsonElement key)
        {
            string DBJsonValue = null;
            string DBJsonkey = null;
            var obj = JsonConvert.DeserializeObject<dynamic>(key.ToString());
            var obj1 = JObject.Parse(key.ToString());
            var model = obj.model.ToString();
            var data = obj.data.ToString();
            int count = 0;
            dynamic result;

            foreach (JProperty item in obj.data)
            {
                var isInt = int.TryParse(item.Value.ToString(), out int vv);
                if (count > 0)
                {
                    DBJsonValue += ',';
                    DBJsonkey += ',';
                }
                DBJsonkey += item.Name;
                DBJsonValue += isInt == true ? $"(p->>'{ item.Name.ToString()}')::int" : $"(p->>'{ item.Name.ToString()}')";
                count++;
            }
            try
            {
                var extracdata = new ExtractData<application>(config);
                result = extracdata.insertdataJson(model, data, DBJsonValue, DBJsonkey);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{model}/{Id:int}")]
        public IActionResult GetPage(string model, int Id)
        {
            var totalPage = this.inserttable.getData("rest_getbytotalrow", model);
            var obj = JObject.Parse(totalPage.ToString());
            var compose = new ComposePage(Id, int.Parse((obj["rows"]).ToString()));
            var result = compose.Wrapper();
            //      var p = new List<test> { new test { one = "io", tree = "loro" } , new test { one = "io", tree = "loro" }, new test { one = "io", tree = "loro" } };
            return Ok(result);
        }

        public partial class test
        {
            internal string one { get; set; }
            internal string tree { get; set; }
        }
    }
}



//A JToken is a generic representation of a JSON value of any kind. It could be a string, object, array, property, etc.
//A JProperty is a single JToken value paired with a name. It can only be added to a JObject, and its value cannot be another JProperty.
//A JObject is a collection of JProperties. It cannot hold any other kind of JToken directly.
