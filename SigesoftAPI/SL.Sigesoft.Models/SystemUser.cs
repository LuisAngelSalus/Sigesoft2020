using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            Permission = new HashSet<Permission>();
            Quotation = new HashSet<Quotation>();
            Secuential = new HashSet<Secuential>();
            Suscription = new HashSet<Suscription>();
            Notification = new HashSet<Notification>();
            Company = new HashSet<Company>();
        }

        public int i_SystemUserId { get; set; }
        public int i_PersonId { get; set; }
        public string v_UserName { get; set; }
        public string v_Password { get; set; }
        public string v_Email { get; set; }
        public string v_Phone { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<Quotation> Quotation { get; set; }
        public virtual ICollection<Secuential> Secuential { get; set; }
        public virtual ICollection<Suscription> Suscription { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<Company> Company { get; set; }
    }
}
