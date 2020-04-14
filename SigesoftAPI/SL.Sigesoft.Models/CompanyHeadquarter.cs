using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SL.Sigesoft.Models
{
    public partial class CompanyHeadquarter
    {
        public CompanyHeadquarter()
        {
            Quotation = new HashSet<Quotation>();
            Warehouse = new HashSet<Warehouse>();
        }

        public int i_CompanyHeadquarterId { get; set; }
        public int i_CompanyId { get; set; }
        public string v_Name { get; set; }
        public string v_Address { get; set; }
        public string v_PhoneNumber { get; set; }
        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Quotation> Quotation { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
