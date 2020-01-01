﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class ProfileComponent
    {
        public int i_ProfileComponentId { get; set; }
        public int? i_CategoryId { get; set; }
        public string v_ComponentId { get; set; }
        public float r_MinPrice { get; set; }
        public float r_PriceList { get; set; }
        public float r_Sale_Price { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}