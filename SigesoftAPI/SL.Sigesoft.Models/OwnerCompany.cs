using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class OwnerCompany
    {
        public OwnerCompany()
        {
            Access = new HashSet<Access>();
        }

        public int i_OwnerCompanyId { get; set; }
        public string v_BusinessName { get; set; }
        public string v_IdentificationNumber { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<Access> Access { get; set; }
    }
}
