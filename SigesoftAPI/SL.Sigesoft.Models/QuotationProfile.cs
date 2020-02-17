using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SL.Sigesoft.Models
{
    public partial class QuotationProfile
    {
        public QuotationProfile()
        {
            ProfileComponent = new HashSet<ProfileComponent>();
        }

        public int i_QuotationProfileId { get; set; }
        public int i_QuotationId { get; set; }
        public string v_ProfileName { get; set; }
        public int i_ServiceTypeId { get; set; }
        public int i_TypeFormatId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }

        public virtual Quotation Quotation { get; set; }
        public virtual ICollection<ProfileComponent> ProfileComponent { get; set; }
    }
}
