using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class PriceList
    {
        public int i_PriceListId { get; set; }
        public int i_CompanyId { get; set; }
        public string v_ComponentId { get; set; }
        public decimal r_Price { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Company Company { get; set; }
    }
}
