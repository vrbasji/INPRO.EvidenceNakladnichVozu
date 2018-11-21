using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ChangeHistory
    {
        public int ChangeHistoryId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string NameOfParameter { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public virtual Car Car { get; set; }
    }
}
