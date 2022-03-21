using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ecosystem.Entity;
using Ecosystem.RepoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;

namespace Ecosystem.Services
{
    public class ProcessConstructor
    {
        IInsertTable<service> _insert;
        public ProcessConstructor(IInsertTable<service> insert)
        {
            _insert = insert;
        }
        public object ProcHelperTemplate(HttpRequest Request, string procName,string data, object fromBody = null, bool commit = false, string userProfile = "")
        {
            var dict_params = new Dictionary<string, string>();
            if (fromBody != null)
                dict_params.Add("json_in", fromBody.ToString());

            var kvParams = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            kvParams.AllKeys.ToList().ForEach(x => dict_params.Add(x, kvParams[x]));

            object res = _insert.getData( procName,data);

            //var jObj = JObject.Parse(res);
            //var containerToJson = JsonConvert.DeserializeObject<JObject>(res);
            //var container = JsonConvert.DeserializeObject<List<container>>(containerToJson["data"].ToString());
            //            return $"{{ \"status\":{StatusCodes.Status200OK},\"body\" :{jObj.ToString()}}}";
            return res;
        }
    }
}
