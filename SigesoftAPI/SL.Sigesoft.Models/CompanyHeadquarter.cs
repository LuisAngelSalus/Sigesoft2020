using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class CompanyHeadquarter
    {
        public CompanyHeadquarter()
        {
            Company = new HashSet<Company>();
        }

        public int i_CompanyHeadquarterId { get; set; }
        public int i_CompanyId { get; set; }
        public string v_Name { get; set; }
        public string v_Address { get; set; }
        public string v_PhoneNumber { get; set; }      
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<Company> Company { get; set; }
    }
}
