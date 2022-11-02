using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DAL.ORM.Entity
{
    public class Product : BaseEntity
    {

        public Product()
        {
            this.Suppliers = new HashSet<Supplier>();
        }

        public virtual ICollection<Supplier> Suppliers { get; set; }


        public string Name { get; set; }
  
        public string Description { get; set; }

        public int UnitsInStock { get; set; }

        public decimal UnitPrice { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
