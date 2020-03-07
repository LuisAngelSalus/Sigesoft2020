using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class ClientUser
    {
        public int i_ClientUserId { get; set; }
        public int i_CompanyId { get; set; }
        public string v_UserName { get; set; }
        public string v_Password { get; set; }
        public string v_FullName { get; set; }
        public int i_UserTypeId { get; set; }
        public int i_TypeDocumentId { get; set; }
        public string v_NroDocument { get; set; }
        public string v_NroCpm { get; set; }
        public string v_MobileNumber { get; set; }
        public string v_Email { get; set; }
        public int i_IsActive { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
