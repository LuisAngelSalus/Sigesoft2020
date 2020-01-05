using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class ProfileDetail
    {
        public int i_ProfileDetailId { get; set; }
        public int i_ProtocolProfileId { get; set; }
        public string v_ComponentId { get; set; }
        public int i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }
        public decimal? r_MinPrice { get; set; }
        public decimal? r_ListPrice { get; set; }
        public decimal? r_SalePrice { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
