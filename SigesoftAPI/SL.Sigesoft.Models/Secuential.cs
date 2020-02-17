using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Secuential
    {
        public int i_SecuentialId { get; set; }
        public int i_OwnerCompanyId { get; set; }
        public int i_SystemUserId { get; set; }
        public string v_Process { get; set; }
        public int i_Secuential { get; set; }

        public virtual OwnerCompany OwnerCompany { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
