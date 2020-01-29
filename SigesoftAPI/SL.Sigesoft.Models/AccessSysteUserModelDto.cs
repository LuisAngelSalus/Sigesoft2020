using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class AccessSysteUserModelDto
    {
        public int SystemUserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public List<Companies> Companies { get; set; }
    }

    public class Companies
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<Roles>Roles { get; set; }
    }

    public class Roles
    {
        public int RolId { get; set; }
        public string RolName { get; set; }
        public List<Module> Modules { get; set; }
    }

    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
    }

}
