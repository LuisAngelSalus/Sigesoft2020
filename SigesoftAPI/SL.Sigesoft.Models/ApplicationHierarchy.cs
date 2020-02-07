using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class ApplicationHierarchy
    {
        public ApplicationHierarchy()
        {
            Profile = new HashSet<Profile>();
        }

        public int i_ApplicationHierarchyId { get; set; }
        public string v_Description { get; set; }
        public string v_Path { get; set; }
        public int? i_ParentId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
    }
}
