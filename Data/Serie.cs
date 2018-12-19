using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Serie
    {
        public int SerieId { get; set; }
        public float Weight { get; set; }
        public float MaxSpeed { get; set; }
        public int NumberOfAxle { get; set; }
        public int BumpersLenght { get; set; }
        public int Lenght { get; set; }
        public int Area { get; set; }
        public float Space { get; set; }
        public float Wheelbase { get; set; }
        public float ConstructionWeight { get; set; }
        public float AreaHeight { get; set; }
        public int NumberOfGear { get; set; }
        public int NumberOfPars { get; set; }
        public int AirBreakWeight { get; set; }
        public int HandBreakWeight { get; set; }

        public virtual AirBreak AirBreak { get; set; }
        public virtual HandBreak HandBreak { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
