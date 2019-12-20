using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Role
    {
        public Role()
        {
            Permission = new HashSet<Permission>();
            Profiles = new HashSet<Profile>();
        }

        public int i_RoleId { get; set; }
        public string v_Description { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
