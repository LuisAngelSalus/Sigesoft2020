using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class QuoteTrackingRegisterDto
    {
        public int QuotationId { get; set; }
        public string Commentary { get; set; }
        public string StatusName { get; set; }        
        public int InsertUserId { get; set; }
    }
}
