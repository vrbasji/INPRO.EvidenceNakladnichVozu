using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repair
    {
        public int RepairId  { get; set; }
        public DateTime RepairDate { get; set; }
        public string Description { get; set; }

        public virtual Fault Fault { get; set; }
    }
}
