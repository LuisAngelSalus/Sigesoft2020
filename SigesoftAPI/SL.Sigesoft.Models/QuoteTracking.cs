﻿using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class QuoteTracking
    {
        public int i_QuoteTrackingId { get; set; }
        public int i_QuotationId { get; set; }
        public DateTime d_Date { get; set; }
        public string v_Commentary { get; set; }
        public string v_StatusName { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Quotation Quotation { get; set; }
    }
}
