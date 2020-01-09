using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class QuotationFilterModel
    {
        public int QuotationId { get; set; }
        public string NroQuotation { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public string CompanyName { get; set; }
        public decimal Total { get; set; }
        public string StatusName { get; set; }
        public DateTime? USDate { get; set; }
        public string TrackingDescription { get; set; }
    }
}
