using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Accesses = new HashSet<Access>();
        }

        public int i_PermissionId { get; set; }
        public int? i_SystemUserId { get; set; }
        public int? i_RoleId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Role Roles { get; set; }
        public virtual SystemUser i_SystemUser { get; set; }
        public virtual ICollection<Access> Accesses { get; set; }
    }
}
