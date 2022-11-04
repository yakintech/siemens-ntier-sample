using Siemens.DAL.ORM.Context;
using Siemens.DAL.ORM.Entity.WebUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.BLL.Service.Role
{
    public class RoleService
    {
        public static bool RoleControl(string email,int roleNumber)
        {
            SiemensContext siemensContext = new SiemensContext();

            WebUser webUser = siemensContext.WebUsers.FirstOrDefault(q => q.EMail == email && q.IsDeleted == false);

            bool roleStatus = false;
            if (webUser != null)
            {
                string[] roles = webUser.Roles?.Split(';');

                foreach (var item in roles)
                {
                    if (item == roleNumber.ToString())
                    {
                        roleStatus = true;
                    }
                }

                return roleStatus;

            }
            else
            {
                return false;
            }
        }
    }
}
