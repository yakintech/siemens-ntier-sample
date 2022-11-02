using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Dto.Models.Response
{
    public class SupplierListResponseDto : BaseDto
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }

        public SupplierListAddressResponseDto SupplierListAddressResponseDto { get; set; }

        public List<SupplierListProductResponseDto> Products { get; set; }


    }

    public class SupplierListProductResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SupplierListAddressResponseDto
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
