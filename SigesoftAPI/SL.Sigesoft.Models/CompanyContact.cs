using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SL.Sigesoft.Models
{
   public partial class CompanyContact
    {

    public int i_CompanyContactId { get; set; }
    public int i_CompanyHeadquarterId { get; set; }
    public string v_FullName { get; set; }
    public string v_TypeUs { get; set; }
    public string v_Dni { get; set; }
    public string v_CM { get; set; }
    public string v_Phone { get; set; }
    public string v_Email { get; set; }      
    public YesNo i_IsDeleted { get; set; }
    public int? i_InsertUserId { get; set; }
    public DateTime? d_InsertDate { get; set; }
    public int? i_UpdateUserId { get; set; }
    public DateTime? d_UpdateDate { get; set; }
        
    [NotMapped]
    public string v_CompanyHeadquarterName { get; set; }
    }
}
