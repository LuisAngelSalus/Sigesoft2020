using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Schedule
    {
        public int i_ScheduleId { get; set; }
        public int i_ServiceId { get; set; }
        public DateTime d_DateTimeCalendar { get; set; }
        public DateTime? d_CircuitStartDate { get; set; }        
        public int i_CalendarStatusId { get; set; }        
        public int i_IsVipId { get; set; }
        public int i_moodId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Service Service { get; set; }
    }
}
