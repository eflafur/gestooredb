using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.Entity
{
    public class application_datacenter
    {
        public int id { get; set; }
        public application application { get; set; }
        public datacenter datacenter { get; set; }

        [ForeignKey("application")]
        public int application_id { get; set; }
        [ForeignKey("datacenter")]
        public int datacenter_id { get; set; }

    }
}
