using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class SystemUserLoginModel
    {
        public int SystemUserId { get; set; }
        public string UserName { get; set; }
        public List<RoleModel> Roles { get; set; }
    }

    public class RoleModel
    {
        public string RolName { get; set; }
    }
}
