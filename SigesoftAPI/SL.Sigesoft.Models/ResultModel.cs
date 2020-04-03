using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class ResultModel
    {
        public int i_ServiceId { get; set; }
        public DateTime d_ServiceDate { get; set; }
        public int i_ProtocolId { get; set; }
        public string v_ProtocolName { get; set; }
        public int i_PersonId { get; set; }
        public string v_FirstName { get; set; }
        public string v_FirstLastName { get; set; }
        public string v_SecondLastName { get; set; }
        public string FullName { get; set; }
        public int i_CompanyId { get; set; }
        public string v_Name { get; set; }
        public string v_CurrentPosition { get; set; }
        public int i_ServiceStatusId { get; set; }
        public string ServiceStatusClass { get; set; }
        public string v_ValueService { get; set; }
        public int i_AptitudeStatusId { get; set; }
        public string v_ValueAptitude { get; set; }

    }
}
