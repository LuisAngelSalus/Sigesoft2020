using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class ProtocolProfileRegisterDto
    {        
        public string Name { get; set; }
        public List<ProfileDetailRegisterDto> ProfileDetail { get; set; }        
    }

    public class ProfileDetailRegisterDto
    {
        //public int ProfileDetailId { get; set; }
        //public int ProtocolProfileId { get; set; }
        public string ComponentId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? SalePrice { get; set; }
    }

}
