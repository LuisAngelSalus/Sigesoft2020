using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class QuotationProfile
    {
        public QuotationProfile()
        {
            ProfileComponents = new List<ProfileComponent>();
        }

        public int i_QuotationProfileId { get; set; }
        public int? i_QuotationId { get; set; }
        public int? i_ProfileId { get; set; }
        public int? i_ServiceTypeId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }

        public List<ProfileComponent> ProfileComponents { get; set; }
    }
}
