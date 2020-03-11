using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Schedule
    {
        public Schedule()
        {
            Service = new HashSet<Service>();
        }

        public int i_ScheduleId { get; set; }
        public int i_WorkerId { get; set; }
        public int i_ServiceId { get; set; }
        public DateTime d_DateTimeCalendar { get; set; }
        public DateTime? d_CircuitStartDate { get; set; }
        public DateTime? d_EntryTimeCM { get; set; }
        public int i_ServiceTypeId { get; set; }
        public int i_CalendarStatusId { get; set; }
        public int i_ServiceModeId { get; set; }
        public int i_ProtocolId { get; set; }
        public int i_NewContinuationId { get; set; }
        public int i_LineStatusId { get; set; }
        public int i_IsVipId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<Service> Service { get; set; }
    }
}
