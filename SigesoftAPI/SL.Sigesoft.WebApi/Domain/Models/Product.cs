using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SL.Sigesoft.WebApi.Domain.Models
{
    [Table("Product", Schema = "logistics")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityPackage { get; set; }
    }
}
