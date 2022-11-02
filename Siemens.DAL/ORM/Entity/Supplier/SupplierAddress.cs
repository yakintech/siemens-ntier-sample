using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DAL.ORM.Entity
{
    public class SupplierAddress : BaseEntity
    {
        [ForeignKey("Supplier")]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
