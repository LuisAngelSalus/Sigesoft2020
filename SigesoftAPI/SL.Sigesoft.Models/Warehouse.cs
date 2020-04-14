using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class Warehouse
    {
        public int i_WarehouseId { get; set; }
        public int? i_CompanyId { get; set; }
        public int? i_CompanyHeadquarterId { get; set; }
        public string v_Description { get; set; }
        public int i_IsPrincipal { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyHeadquarter CompanyHeadquarter { get; set; }
    }
}
