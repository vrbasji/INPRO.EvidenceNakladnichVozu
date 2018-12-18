using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public DateTime LastRevision { get; set; }
        public int RevisionPeriod { get; set; }
        public DateTime LastZTE { get; set; }
        public DateTime LastZTL { get; set; }
        public bool Certification { get; set; }
        public State State { get; set; }

        public virtual GoodGroup GoodGroup { get; set; }
        public virtual Serie Serie { get; set; }
        public virtual Subject ServiceResponsiblePerson { get; set; }
        public virtual Subject Owner { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Revision> Revisions { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Fault> Faults { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ChangeHistory> ChangeHistories { get; set; }
    }
}
