using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DAL.ORM.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
