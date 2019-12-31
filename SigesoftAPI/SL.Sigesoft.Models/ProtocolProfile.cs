using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public partial class ProtocolProfile
    {
        public ProtocolProfile()
        {
            ProfileDetail = new HashSet<ProfileDetail>();
        }

        public int i_ProtocolProfileId { get; set; }
        public string v_Name { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<ProfileDetail> ProfileDetail { get; set; }
    }
}
