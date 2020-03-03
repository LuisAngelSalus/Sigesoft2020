using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models.Win
{
   public class ProtocolWin
    {
       public string v_ProtocolId { get; set; }
        public string v_Name { get; set; }
        public int v_StatusProtocolId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public int i_EsoTypeId { get; set; }
        public string v_GroupOccupationId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }

        public string v_WorkingLocationId { get; set; }
        public string v_CostCenter { get; set; }
        public int i_MasterServiceTypeId { get; set; }
        public int i_MasterServiceId { get; set; }
        public int i_HasVigency { get; set; }
        public int? i_ValidInDays { get; set; }
        public int i_IsActive { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int i_InsertUserId { get; set; }
        public DateTime d_InsertDate { get; set; }

        public int i_UpdateUserId { get; set; }
        public DateTime d_UpdateDate { get; set; }
        public int i_ProfileId { get; set; }
        public int i_TypeReport { get; set; }

    }
}
