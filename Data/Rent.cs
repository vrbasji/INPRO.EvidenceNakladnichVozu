using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Rent
    {
        public int RentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RentType RentType { get; set; }

        public virtual Car Car { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
