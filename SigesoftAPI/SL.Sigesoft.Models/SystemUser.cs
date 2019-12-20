using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            Permissions = new HashSet<Permission>();
        }

        public int i_SystemUserId { get; set; }
        public int? i_PersonId { get; set; }
        public string v_UserName { get; set; }
        public string v_Password { get; set; }
        public string v_Email { get; set; }
        public string v_Phone { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
