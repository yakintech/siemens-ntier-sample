using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DAL.ORM.Entity.WebUser
{
    public class WebUser : BaseEntity
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
        public string Roles { get; set; }
    }
}
