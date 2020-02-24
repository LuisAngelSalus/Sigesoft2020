using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class AccountSetting
    {
        public int i_AccountSettingId { get; set; }
        public int i_SystemUserId { get; set; }
        public int i_OwnerCompanyId { get; set; }
        public int i_RoleId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
