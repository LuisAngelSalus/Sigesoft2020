using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Component
    {
        public int i_ComponentId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_Name { get; set; }        
        public int i_CategoryId { get; set; }        
        public decimal r_CostPrice { get; set; }
        public decimal r_BasePrice { get; set; }
        public decimal r_SalePrice { get; set; }     
    }
}
