using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class QuotationProfile
    {
        public int i_QuotationProfileId { get; set; }
        public int? i_QuotationId { get; set; }
        public int? i_ProfileId { get; set; }
        public int? i_ServiceTypeId { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
