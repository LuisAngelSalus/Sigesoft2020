using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class WarehouseUpdateDataDto
    {
        public int WarehouseId { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int CompanyHeadquarterId { get; set; }
        public int UpdateUserId { get; set; }
    }
}
