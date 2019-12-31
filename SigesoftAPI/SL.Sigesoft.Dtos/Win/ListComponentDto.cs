using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos.Win
{
   public class ListComponentDto
    {
        public string ComponentId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public float? CostPrice { get; set; }
        public float? BasePrice { get; set; }
        public float? SalePrice { get; set; }
    }
}
