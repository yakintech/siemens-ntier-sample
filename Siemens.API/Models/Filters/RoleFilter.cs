using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Siemens.BLL.Service;
using Siemens.BLL.Service.Role;
using System.Security.Claims;

namespace Siemens.API.Models.Filters
{
    public class RoleFilter : AuthorizeAttribute, IAuthorizationFilter
    {

        int role;
        public RoleFilter(int roleNumber)
        {
            this.role = roleNumber;
        }

       

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var emailClaim = context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email);

            if (emailClaim != null)
            {
                string email = emailClaim.Value;

                bool result = RoleService.RoleControl(email, role);

                if (!result)
                {

                  

                }
             


            }

        }
    }
}
