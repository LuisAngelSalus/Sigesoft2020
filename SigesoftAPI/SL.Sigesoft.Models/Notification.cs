using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Notification
    {
        public int i_NotificationId  { get; set; }
        public int i_SystemUserId { get; set; }
        public string v_Title { get; set; }
        public string v_Message { get; set; }
        public int i_PriorityLevelId { get; set; }
        public DateTime d_ShippingDate { get; set; }
        public int? i_WasRead { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual SystemUser SystemUser { get; set; }
    }
}
