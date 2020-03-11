using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class UpdateAccessModel
    {
        public int UserId { get; set; }
        public int OwnerCompanyId { get; set; }
        public int[] Roles { get; set; }
        public int UpdateUserId { get; set; }

        public int InsertUserId { get; set; }
    }
}
