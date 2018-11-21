using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ICO { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Car> OwnedCars { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Rent> RentedCars { get; set; }
    }
}
