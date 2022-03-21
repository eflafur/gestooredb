using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.Entity
{
    public class application
    {
        public int id { get; set; }
        public string name { get; set; }
        public int port { get; set; }
        public string url { get; set; }
        public userprofile userprofile { get; set; }
        [ForeignKey("userprofile")]
        public int userprofile_id { get; set; }
    }
}
