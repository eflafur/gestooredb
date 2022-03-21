using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem.Model
{
    public class ChainBlock
    {
        public string Leader { get; set; }
        public List<ChainBlock> Worker;
    }
}
