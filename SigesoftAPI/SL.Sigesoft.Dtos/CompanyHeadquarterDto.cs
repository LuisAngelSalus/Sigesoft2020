using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class CompanyHeadquarterDto
    {
        public int CompanyHeadquarterId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public RecordStatus RecordStatus { get; set; } = RecordStatus.Grabado;
        public RecordType RecordType { get; set; } = RecordType.NoTemporal;

    }
}
