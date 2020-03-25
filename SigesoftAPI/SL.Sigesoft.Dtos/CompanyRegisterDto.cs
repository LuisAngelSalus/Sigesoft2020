using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class CompanyRegisterDto
    {
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string Address { get; set; }
        public string PathLogo { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public string Mail { get; set; }
        public string District { get; set; }
        public string PhoneCompany { get; set; }
        public int ResponsibleSystemUserId { get; set; }
        public int InsertUserId { get; set; }

        public List<CompanyHeadquarterDto> companyHeadquarter { get; set; }

    }
}
