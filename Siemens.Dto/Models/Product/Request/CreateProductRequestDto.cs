using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Dto.Models
{
    public class CreateProductRequestDto
    {

        public Guid CategoryId { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Stock { get; set; }

    }
}
