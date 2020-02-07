using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class SystemUserRegisterDto
    {
        public string UserName { get; set; }
        public string PersonId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
