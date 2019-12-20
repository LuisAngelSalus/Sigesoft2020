using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class ListSystemUserDto
    {
        public ListSystemUserDto()
        {
            permissions = new List<PermissionDto>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<PermissionDto> permissions { get; set; }
    }
}
