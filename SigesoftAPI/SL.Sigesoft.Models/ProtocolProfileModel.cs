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
        public List<CategoryModel> UnselectedCategories { get; set; }
    }

    public class CategoryModel{
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProfileDetailModel> Detail { get; set; }
    }

    public class ProfileDetailModel
    {
        public int CategoryId { get; set; }
        public bool Active { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public float? MinPrice { get; set; } 
        public float? ListPrice { get; set; }
        public float? SalePrice { get; set; }
    }
}
