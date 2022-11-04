using Siemens.BLL.Service.Repositories.Interfaces;
using Siemens.DAL.ORM.Context;
using Siemens.DAL.ORM.Entity.WebUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.BLL.Service.Repositories.Entity
{
    public class WebUserRepository : GenericRepository<WebUser>, IWebUserRepository
    {
        public WebUserRepository(SiemensContext context) : base(context)
        {

        }

    }
}
