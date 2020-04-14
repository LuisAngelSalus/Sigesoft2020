using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyHeadquarter = new HashSet<CompanyHeadquarter>();
            PriceList = new HashSet<PriceList>();
            Protocol = new HashSet<Protocol>();
            Quotation = new HashSet<Quotation>();
            Warehouse = new HashSet<Warehouse>();
        }

        public int i_CompanyId { get; set; }
        public int? i_ResponsibleSystemUserId { get; set; }
        public string v_Name { get; set; }
        public string v_IdentificationNumber { get; set; }
        public string v_Address { get; set; }
        public string v_PhoneNumber { get; set; }
        public string v_ContactName { get; set; }
        public string v_Mail { get; set; }
        public string v_District { get; set; }
        public string v_PhoneCompany { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<CompanyHeadquarter> CompanyHeadquarter { get; set; }
        public virtual ICollection<PriceList> PriceList { get; set; }
        public virtual ICollection<Protocol> Protocol { get; set; }
        public virtual ICollection<Quotation> Quotation { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
