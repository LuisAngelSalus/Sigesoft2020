using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class Secuential
    {
        [Key]
        public int i_SecuentialId {get;set;}
        public int i_OwnerCompanyId { get; set; }
        public int i_SystemUserId { get; set; }
        public string v_Process { get; set; }
        public int i_Secuential { get; set; }
    }
}
