using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Main.Models
{
    public class RentModel
    {
        public int CarId { get; set; }
        public int SubjectId { get; set; }
        public DateTime DueDate { get; set; }
    }
}