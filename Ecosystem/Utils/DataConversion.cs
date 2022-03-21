using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.Utils
{
    public class DataConversion
    {
        public static List<Dictionary<string, object>> DataTable2DictList(DataTable dt)
        {
            List<Dictionary<string, object>> lstRes = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    var jsonString = "{'FirstName': 'Olivia', 'LastName': 'Mason'}";
                    var jo = JObject.Parse(jsonString);
                    var k1 = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    var k2 = JsonConvert.DeserializeObject<object>(jsonString);

                    var iot = jsonString.GetType();
                    var k1t = k1.GetType();
                    var k2t = k2.GetType();

                    var t = typeof(string);
                    var r = row[col].GetType();
                    if (row[col].GetType() == typeof(string))
                    {
                        string s = row[col].ToString().Trim();
                        JObject parsed = null;
                        if (s.StartsWith("{") && s.EndsWith("}")
                            || s.StartsWith("[") && s.EndsWith("]"))
                        {
                            Console.WriteLine(row[col].GetType().Name);
                            try
                            {
                                parsed = JObject.Parse(s);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        dictRow[col.ColumnName] = parsed ?? (object)s;
                    }
                    else
                    {
                        dictRow[col.ColumnName] = row[col];
                    }
                }
                lstRes.Add(dictRow);
            }

            return lstRes;
        }

        public static string DataTable2JsonString(DataTable dt)
        {
            return JsonConvert.SerializeObject(DataTable2DictList(dt));
        }
    }

    public class utils
    {
        public static IEnumerable<Dictionary<string, object>> dt2dic(DataTable dt)
        {
            var gg1 = dt.Rows.Cast<DataRow>().Select(row => dt.Columns.Cast<DataColumn>().ToDictionary(key => key.ToString(), value => row[value] as string ));
            var gg = dt.Rows.Cast<DataRow>().Select(row => dt.Columns.Cast<DataColumn>().ToDictionary(key => key.ToString(), value => row[value] ));

            var dic = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {

                var d = new Dictionary<string, object>();
                foreach (var item in dt.Columns)
                {
                    d.Add(item.ToString(), row[item.ToString()]);
                }
                dic.Add(d);
                dt.Columns.ToString();
            }


            var l1 = new List<string> { "io", "tu", "voi", "essi" };
            var d1 = l1.ToDictionary(key => key, value => "ciccio");
            return gg;
        }
    }
}
