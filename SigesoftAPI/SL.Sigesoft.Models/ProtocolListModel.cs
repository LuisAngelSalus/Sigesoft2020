using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class ProtocolListModel
    {
        public int i_ProtocolId { get; set; }
        public int i_CompanyId { get; set; }
        public string v_CompanyName { get; set; }
        public string v_ProtocolName { get; set; }
        public int i_ServiceTypeId { get; set; }
        public string v_ServiceTypeName { get; set; }
        public int i_TypeFormatId { get; set; }
        public string v_TypeFormatName { get; set; }
        public int i_QuotationProfileIdRef { get; set; }
    }
}
