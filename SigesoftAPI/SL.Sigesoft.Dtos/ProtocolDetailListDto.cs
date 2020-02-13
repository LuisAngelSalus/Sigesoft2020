﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class ProtocolDetailListDto
    {
        public int ProtocolDetailId { get; set; }
        public int ProtocolId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? AgeConditionalId { get; set; }
        public int? Age { get; set; }
        public int? GenderConditionalId { get; set; }
        public int? QuotationProfileIdRef { get; set; }
        
    }
}
