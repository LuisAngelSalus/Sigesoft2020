using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models.Win
{
    public class SystemParameterWin
    {
        public int i_GroupId {get; set;}
        public int i_ParameterId { get; set;}
        public string v_Value1 { get; set;}
        public string v_Value2 { get; set;}
        public string v_Field { get; set; }
        public int i_ParentParameterId { get; set; }
        public int i_Sort { get; set;}
        public int i_IsDeleted { get; set;}

    }
}
