using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{

    public class ScheduleRegisterDto
    {
        public DateTime DateTimeCalendar { get; set; }
        public int CalendarStatusId { get; set; }     
        public int IsVipId { get; set; }
        public int MoodId { get; set; }
        public ServiceRegisterDto Service { get; set; }
    }

    public class ServiceRegisterDto
    {
        public ServiceRegisterDto()
        {
            ServiceComponent = new HashSet<ServiceComponentRegisterDto>();
        }        
        public int ProtocolId { get; set; }
        public int WorkerId { get; set; }        
        public int ServiceStatusId { get; set; }        
        public ICollection<ServiceComponentRegisterDto> ServiceComponent { get; set; }
    }

    public class ServiceComponentRegisterDto
    {     
        public string ComponentId { get; set; }
        public int ServiceComponentStatusId { get; set; }
    }
}
