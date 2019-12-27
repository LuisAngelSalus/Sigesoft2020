using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class CompanyDto
    {
        public CompanyDto()
        {
            CompanyHeadquarter = new List<CompanyHeadquarterDto>();
        }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public string Mail { get; set; }

        public List<CompanyHeadquarterDto> CompanyHeadquarter { get; set; }

    }
}
