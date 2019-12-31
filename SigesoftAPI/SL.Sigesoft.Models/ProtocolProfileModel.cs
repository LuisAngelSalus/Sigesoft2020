using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class ProtocolProfileModel
    {
        public int ProtocolProfileId { get; set; }
        public string ProtocolProfileName { get; set; }
        public List<CategoryModel> categories { get; set; }
    }

    public class CategoryModel{
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProfileDetailModel> Detail { get; set; }
    }

    public class ProfileDetailModel
    {
        public bool Active { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public float? CostPrice { get; set; }
        public float? BasePrice { get; set; }
        public float? SalePrice { get; set; }
    }
}
