using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Service
    {        
        public Service()
        {
            ServiceComponent = new HashSet<ServiceComponent>();
        }

        public int i_ServiceId { get; set; }
        public int i_ProtocolId { get; set; }
        public int i_WorkerId { get; set; }
        public int i_ServiceStatusId { get; set; }
        public int i_AptitudeStatusId { get; set; }
        public DateTime d_ServiceDate { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<ServiceComponent> ServiceComponent { get; set; }

    }
}
