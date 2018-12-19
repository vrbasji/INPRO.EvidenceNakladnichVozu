using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Main.Models
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
    }
}