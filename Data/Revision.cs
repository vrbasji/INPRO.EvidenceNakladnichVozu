using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Revision
    {
        public int RevisionId { get; set; }
        public DateTime LastRevisionDate { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Car Car { get; set; }
    }
}
