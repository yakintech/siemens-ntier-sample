using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Dto.Models.Supplier.Request
{
    public class SupplierCreateRequestDto
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public SupplierAddressCreateRequestDto SupplierAddressCreateRequestDto { get; set; }
        public List<Guid> Products { get; set; }
    }

    public class SupplierAddressCreateRequestDto
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }

}
