using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class QuoteTrackingUpdateDto
    {
        public int QuoteTrackingId { get; set; }
        public string Commentary { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
