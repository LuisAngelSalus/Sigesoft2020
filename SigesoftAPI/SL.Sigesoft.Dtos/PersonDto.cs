using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SL.Sigesoft.Dtos
{
   public class PersonDto
    {
        public int i_PersonId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombres")]
        public string v_FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido Paterno es requerido")]
        [Display(Name = "ApellidoPaterno")]
        public string v_FirstLastName { get; set; }
        [Required(ErrorMessage = "El Apellido Maternos es requerido")]
        [Display(Name = "ApellidoMaterno")]
        public string v_SecondLastName { get; set; }

    }
}
