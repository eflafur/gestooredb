using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Ecosystem.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ecosystem.RepoService;
using Ecosystem.Services;
using System.Text.Json;
using System.Reflection;
using DataExchange;

namespace Ecosystem.Controllers
{

    public delegate string del1<T>(T ser, string proc);

    [Route("api/[controller]")]
    [ApiController]
    public class Device : ControllerBase
    {
        private const string USER_PROFILE_NOT_AUTHORIZED = "The User is not authorized to perform this command.";
        private const string STATUS_CODE_FIELD = "_statuscode";

        private IConfiguration _config;
        private IInsertTable<userprofile> _insertDataUser;
        private IInsertTable<service> _insertDataService;
        private IInsertTable<container> _insertDataContainer;
        private readonly ProcessConstructor _process;
        private readonly Iterative iterative;
        private readonly RedisController redis;
        IOTManager _db;

        private string GetUserProfile() =>
          User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("profile"
              , StringComparison.InvariantCultureIgnoreCase))?.Value;

        private string GetUserName() =>
            User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("id"
                , StringComparison.InvariantCultureIgnoreCase))?.Value;

        public Device(IConfiguration config, IInsertTable<userprofile> insertDataUser, IInsertTable<service> insertDataService,
            IInsertTable<container> insertDataContainer, ProcessConstructor process, Iterative iterative, RedisController redis)
        {
            _config = config;
            _insertDataUser = insertDataUser;
            this._process = process;
            this.iterative = iterative;
            this.redis = redis;
            this._insertDataService = insertDataService;
            this._insertDataContainer = insertDataContainer;
        }

        //   [Authorize("creedegon")]
        [HttpGet("{schema}/{resource}/{data}")]
        public IActionResult Get(string schema, string resource, string data)
        {
            var user = (userprofile)HttpContext.Items["User"];
            var profile = HttpContext.Items["profile"];
            var procName = $"{schema}.rest_{resource}";
            profile = "manager";
            var o = new { test = "cat" };
            var s = o.test;
            dynamic obj = new JObject();
            obj.name = "maria";
            obj.site = "london";

            var res = _process.ProcHelperTemplate(Request, procName, data, o, userProfile: profile.ToString());
            JObject res2 = JsonConvert.DeserializeObject<JObject>(res.ToString());

            var r1 = JObject.Parse(res.ToString());
            var r2 = JsonConvert.DeserializeObject<List<service>>(res2["data"].ToString());
            var r3 = JsonConvert.SerializeObject(r2);

            var tt = r1["data"];
            return Ok(r1["data"].ToString());
        }
        //     [Authorize("creedegon")]
        [Route("postcontainersp")]
        public IActionResult PostContainerSp([FromBody] JsonElement container)
        {

            //TEST JSON
            JArray array = new JArray();
            array.Add("Manual text");
            array.Add(new DateTime(2000, 5, 23));
            JObject o = new JObject();
            o["MyArray"] = array;
            string json = o.ToString();


            dynamic product = new JObject();
            product.id = 33;
            product.name = "name";
            product.idenv = "Elbow Grease";
            product.type = "tipo";
            product.container_id = "container";
            product.tenant_id = "tenant";
            product.servicecategory_id = "category";


            string json12 = "{\"CPU\":\"intel\",\"Drives\":[\"DVD readwriter\",\"500 gigabyte hard drive\" ]}";

            JObject oo12 = JObject.Parse(json12);
            var t = oo12["CPU"];
            var r2 = JsonConvert.DeserializeObject<object>(json12);
            var r22 = JsonConvert.DeserializeObject<object>(r2.ToString());

            var result = _insertDataUser.insertDataSp(container.ToString());
            return Ok(result);
        }
        //     [Authorize("creedegon")]
        [Route("postservicejson")]
        public IActionResult PostServiceJson([FromBody] JsonElement content)
        {
            dynamic entity = null;
            var contentToJson = JToken.Parse(content.ToString());
            var model = contentToJson["model"].ToString();
            var data = contentToJson["data"];
            string result = null;
            //crea istanza da hhtcontext
            //var kk = HttpContext.RequestServices;
            //dynamic ob = (IInsertTable<Service>)kk.GetService(typeof(IInsertTable<Service>));

            //crea istanza
            //Type type = typeof(InsertTable<Service>);
            //var obj = Activator.CreateInstance(null, "Ecosystem.RepoService.InsertTable<User>");
            //var t = obj.Unwrap();
            //dynamic TT = (InsertTable<User>)t;
            switch (model)
            {
                case "service":
                    entity = (service)JsonConvert.DeserializeObject<service>(data.ToString());
                    result = _insertDataService.insertData(entity);
                    break;
                case "container":
                    entity = (container)JsonConvert.DeserializeObject<container>(data.ToString());
                    result = _insertDataContainer.insertData(entity);
                    break;
                case "user":
                    entity = (userprofile)JsonConvert.DeserializeObject<userprofile>(data.ToString());
                    result = _insertDataUser.insertData(entity);
                    break;
            }
            ////DELEGATE
            //var tt=new InsertTable<Service>(_config);
            //var ser = new Service { IdEnv = "ww", IdService = "ee", Name = "pp", Type = "dd" };
            //Type type = typeof(User);
            //del1<Service> del = tt.insertData;
            //tt.insertData(ser);

            return Ok(result);
        }

        //[Authorize("creedgeon")]
        [HttpPost]
        [Route("getdatacriteria")]
        public IActionResult GetDataCriteria(JsonElement element)
        {
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(JObject.Parse(element.ToString()).ToString());
            var tt = JObject.Parse(element.ToString());
            string Params = "0=0";
            bool isParam = false;
            string dbModelJoined = null;
            var paramsCriteria = new Dictionary<string, string>();
            var paramsSelected = new List<string>();

            if (obj.model.ToString() == "organigram")
            {
                var queryHierarchi = $"select *from {obj.model}";

                var res = _insertDataUser.GetDataByParam(queryHierarchi);
                var node = iterative.CollapseData(res);
                return Ok(node);
            }

            foreach (JProperty property in obj["data"])
            {
                var alias = property.Name.Split('.').Count() == 2 ? $"{property.Name} {property.Name.Split('.')[0]}_{property.Name.Split('.')[1]}" : null;
                paramsSelected.Add(alias);
                //if (!string.IsNullOrEmpty(property.Value.ToString()) &&  property.Value.ToString()!="null")
                if (!string.IsNullOrEmpty(property.Value.ToString()) &&  property.Value.ToString()!="null")
                {
                    paramsCriteria.Add(property.Name, "'" + property.Value.ToString() + "'");
                    isParam = true;
                }
            }
            Params = isParam ? string.Join(" and ", paramsCriteria.Select(k => string.Format("{0}={1}", k.Key, k.Value))) : "0=0";

            var dbModel = _config.GetSection("dbrelation").Get<Dictionary<string, List<string>>>();

            //var isMultyRelation = dbModel.Where(k => k.Key == obj.model.ToString()).ToDictionary(k => k.Value, Value => "ciccio");     
            //var node = dbModel.Where(k => k.Key == tt).ToDictionary(x => x.Key, x => test.createJoinQuery(x.Value));
            //var t = dbModel.ToDictionary(k => k.Key, value => value);
            //var ttt = dbModel.First(k => k.Key == "service").Value.ToDictionary(kk => kk, Value => "ciccio");

            foreach (var item in dbModel.Keys)
            {
                var key1 = dbModel[item].Select(k => k.Equals(obj.model.ToString())).ToList();
                if (key1.Count == 2 && key1.Contains(true))
                {
                    dbModel[item].Remove(obj.model.ToString());
                    dbModelJoined = $" left join {item} on {item}.{obj.model.ToString()}_id = {obj.model.ToString()}.id left join  {dbModel[item].First()} on {dbModel[item].First()}.id={item}.{dbModel[item].First()}_id";
                    break;
                }
            }

            var isModel = dbModel.TryGetValue(obj.model.ToString(), out List<string> related);
            if (related?.Count() > 0)
                dbModelJoined += $" left join {string.Join(" left join ", related.Select(kk => string.Format(" {0} on {1}={2}", kk, kk + ".id", obj.model.ToString() + "." + kk + "_id")))}";

            var query = string.Format("select {0} from \"{1}\" {2} where {3} order by {4} limit {5} offset {6}", string.Join(",", paramsSelected), obj.model, dbModelJoined, Params, obj.model + ".id", 10, (int.Parse((obj.page).ToString()) - 1) * 10);
            
            var result = _insertDataUser.GetDataByParam(query);
            return Ok(result);
        }
    }

    public static class test
    {
        public static List<string> tt
        {
            get
            {
                return new List<string> { "io", "tu", "noi" };
            }
        }
        public static List<string> createJoinQuery(List<string> table)
        {
            List<string> gg = new List<string>();

            table.ForEach(x =>
            {
                gg.Add(x + "cioa");
            });
            return gg;
        }
    }
}