using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models.Win
{
   public class ProtocolComponentWin
    {
        public string v_ProtocolComponentId { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_ComponentId { get; set; }
        public decimal r_Price  { get; set; }
        public int i_OperatorId { get; set; }
        public int i_Age { get; set; }
        public int i_GenderId { get; set; }
        public YesNo i_IsConditionalId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int i_InsertUserId { get; set; }
        public DateTime d_InsertDate { get; set; }
        public int i_UpdateUserId { get; set; }
        public DateTime d_UpdateDate  { get; set; }
     
    }
}
