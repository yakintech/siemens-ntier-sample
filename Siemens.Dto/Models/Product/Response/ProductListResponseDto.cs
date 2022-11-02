using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Dto.Models
{
    public class ProductListResponseDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int UnitsInStock { get; set; }

        public CategoryDto Category { get; set; }
    }

    public class CategoryDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}
