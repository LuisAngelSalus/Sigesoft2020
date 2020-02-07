using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Access
    {
        public int i_AccessId { get; set; }
        public int? i_PermissionId { get; set; }
        public int? i_OwnerCompanyId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual OwnerCompany OwnerCompany { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
