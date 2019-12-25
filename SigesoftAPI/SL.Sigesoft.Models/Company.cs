﻿using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Company
    {   
        public int i_CompanyId { get; set; }
        public string v_Name { get; set; }
        public string v_IdentificationNumber { get; set; }
        public string v_Address { get; set; }
        public string v_PhoneNumber { get; set; }
        public string v_ContacName { get; set; }
        public string v_Mail { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual CompanyHeadquarter CompanyHeadquarter { get; set; }
    }
}
