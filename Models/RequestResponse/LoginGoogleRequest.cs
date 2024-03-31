using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LoginGoogleRequest
    {
        public string email { get; set; } = "";
        public string iden { get; set; } = "3215646464646";
    }
}
