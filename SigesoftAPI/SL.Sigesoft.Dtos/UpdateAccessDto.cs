using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class UpdateAccessDto
    {
        public int UserId { get; set; }
        public int OwnerCompanyId { get; set; }
        public int[] Roles { get; set; }
        public int UpdateUserId { get; set; }
    }
}
