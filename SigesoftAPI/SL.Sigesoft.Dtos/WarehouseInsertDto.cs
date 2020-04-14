using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class WarehouseInsertDto
    {
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyHeadquarterId { get; set; }
        public int IsPrincipal { get; set; }
        public int InsertUserId { get; set; }
        //public List<CompanyDto> company { get; set; }
        //public List<CompanyHeadquarterDto> companyHeadquarter { get; set; }
    }
}
