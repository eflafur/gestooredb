using Ecosystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.Services
{
    public class Iterative
    {
        public  ChainBlock CollapseData(dynamic node)
        {
            var container = new Dictionary<string, List<string>>();
            try
            {
                foreach (Dictionary<string, object> item in node)
                {
                    item.TryGetValue("leader", out object resLeader);
                    item.TryGetValue("worker", out object resWorker);
                    var singleValue = resWorker.ToString().Split('"')[1];
                    container.TryGetValue(resLeader.ToString(), out List<string> value);
                    if (value != null)
                    {
                        container[resLeader.ToString()].Add(singleValue);
                        continue;
                    }
                    container.Add(resLeader.ToString(), new List<string> { singleValue });
                }
            }
            catch (Exception ex)
            {

            }
            var result= TreeStructure(container, container.ElementAt(0).Key);
            return result;
            //     var table = node.Select(k => container.Add(k.key.ToString(), k.value.ToString())); 
            //                    foreach(KeyValuePair<string,object  > element in item){
        }

        private  ChainBlock TreeStructure(Dictionary<string, List<string>> tree, string key)
        {
            var node = new ChainBlock();
            tree.TryGetValue(key, out List<string> block);
            node.Leader = key;
            node.Worker = new List<ChainBlock>();

            if (block==null)
                return node;

            tree.Remove(key);

            foreach (var item in block)
            {
                node.Worker.Add(TreeStructure(tree, item));
            };
            return node;
        }
    }
}
