using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Person
    {
        public Person()
        {
            SystemUser = new HashSet<SystemUser>();
        }

        public int i_PersonId { get; set; }
        public string v_FirstName { get; set; }
        public string v_FirstLastName { get; set; }
        public string v_SecondLastName { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<SystemUser> SystemUser { get; set; }
    }
}
