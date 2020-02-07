using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class PersonDto
    {
        public int PersonId { get; set; }        
        public string FirstName { get; set; }        
        public string FirstLastName { get; set; }        
        public string SecondLastName { get; set; }
    }

    public class PersonRegistertDto
    {
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
    }

    public class PersonUpdateDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
    }
}
