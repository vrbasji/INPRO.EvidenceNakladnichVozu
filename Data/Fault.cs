using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Fault
    {
        public int FaultId { get; set; }
        public string Name { get; set; }
        public DateTime FoundedDate { get; set; }
        public string Description { get; set; }

        public virtual Car Car { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
