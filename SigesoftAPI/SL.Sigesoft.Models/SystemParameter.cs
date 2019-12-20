using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class SystemParameter
    {
        public int IGroupId { get; set; }
        public int IParameterId { get; set; }
        public string VValue1 { get; set; }
        public string VValue2 { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
