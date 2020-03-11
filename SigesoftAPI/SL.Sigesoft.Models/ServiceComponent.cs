using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class ServiceComponent
    {
        public int i_ServiceComponentId { get; set; }
        public int i_ServiceId { get; set; }
        public string v_ComponentId { get; set; }
        public int i_ServiceComponentStatusId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Service Service { get; set; }
    }
}
