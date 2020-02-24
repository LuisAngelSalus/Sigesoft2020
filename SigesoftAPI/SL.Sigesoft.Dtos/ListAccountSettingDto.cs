using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class ListAccountSettingDto
    {
        public int AccountSettingId { get; set; }
        public int SystemUserId { get; set; }
        public int OwnerCompanyId { get; set; }
        public int RoleId { get; set; }

    }
}
