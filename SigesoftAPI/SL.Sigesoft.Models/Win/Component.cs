using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SL.Sigesoft.Models.Win
{
    public class Component
    {
        public string v_ComponentId { get; set; }
        public string v_Name { get; set; }
        public int? i_CategoryId { get; set; }
        public float? r_CostPrice { get; set; }
        public float? r_BasePrice { get; set; }
        public float? r_SalePrice { get; set; }
        public int? i_DiagnosableId { get; set; }
        public int? i_IsApprovedId { get; set; }
        public int? i_ComponentTypeId { get; set; }
        public int? i_UIIsVisibleId { get; set; }
        public int? i_UIIndex { get; set; }
        public int? i_ValidInDays { get; set; }
        public int? i_GroupedComponentId { get; set; }
        public YesNo? i_IsDeleted { get; set; }

        [NotMapped]
        public string v_CategoryName { get; set; }
    }
}
