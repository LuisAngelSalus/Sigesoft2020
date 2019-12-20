using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Profile
    {
        public int IProfileId { get; set; }
        public int? i_RoleId { get; set; }
        public int? i_ApplicationHierarchyId { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ApplicationHierarchy ApplicationHierarchy{ get; set; }
        public virtual Role i_Role { get; set; }
    }
}
