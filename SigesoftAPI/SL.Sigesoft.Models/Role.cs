using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Role
    {
        public Role()
        {
            Permission = new HashSet<Permission>();
            Profile = new HashSet<Profile>();
        }

        public int i_RoleId { get; set; }
        public string v_Description { get; set; }
        public string v_PathDashboard { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
