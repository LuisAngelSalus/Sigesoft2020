using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Worker
    {
        public int i_WorkerId { get; set; }
        public int i_PersonId { get; set; }
        public string v_CurrentPosition { get; set; }
        public string v_HomeAddress { get; set; }
        public DateTime d_DateOfBirth { get; set; }
        public int i_GenderId { get; set; }
        public string v_Email { get; set; }
        public string v_MobileNumber { get; set; }
        public int i_TypeDocumentId { get; set; }
        public string v_NroDocument { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int i_InsertUserId { get; set; }
        public DateTime d_InsertDate { get; set; }
        public int i_UpdateUserId { get; set; }
        public DateTime d_UpdateDate { get; set; }

        public virtual Person Person { get; set; }

    }
}
