using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class ProtocolListDto
    {
        public int ProtocolId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ProtocolName { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public int TypeFormatId { get; set; }
        public string TypeFormatName { get; set; }
        public int QuotationProfileIdRef { get; set; }
    }
}
