using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class SystemUserModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Roles { get; set; }
        public int SystemUserId { get; set; }
    }
}
