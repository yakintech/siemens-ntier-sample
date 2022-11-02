using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DAL.ORM.Entity
{
    public class Supplier : BaseEntity
    {

        public Supplier()
        {
            this.Products = new HashSet<Product>();
        }
        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
