using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Protocol
    {
        public Protocol()
        {
            ProtocolDetail = new HashSet<ProtocolDetail>();
        }

        public int i_ProtocolId { get; set; }
        public int i_CompanyId { get; set; }
        public int i_QuotationId { get; set; }
        public string v_ProtocolName { get; set; }
        public int i_ServiceTypeId { get; set; }
        public int i_TypeFormatId { get; set; }
        public int? i_QuotationProfileIdRef { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<ProtocolDetail> ProtocolDetail { get; set; }
    }
}
