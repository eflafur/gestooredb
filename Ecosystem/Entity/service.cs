using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.Entity
{
    public class service
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public container container { get; set; }
        public tenant tenant { get; set; }
        public servicecategory servicecategory { get; set; }
        public string idenv { get; set; }
        [ForeignKey("container")]
        public int container_id { get; set; }
        [ForeignKey("tenant")]
        public int tenant_id { get; set; }
        [ForeignKey("servicecategory")]
        public int servicecategory_id { get; set; }
    }
}
