using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Siemens.API.Models.Auth;
using Siemens.BLL.Service;
using Siemens.Dto.Models.WebUser.Request;

namespace Siemens.API.Controllers
{
    [Route("api/login")]
    public class LoginController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public IActionResult Index(WebUserLoginRequestDto webUserLoginRequestDto)
        {
            var userControl = _unitOfWork.WebUserRepository.Any(q => q.EMail.ToLower() == webUserLoginRequestDto.EMail && q.Password == webUserLoginRequestDto.Password);

            if (userControl)
            {
                //token üretip arkadaşa göndereceğim!
                var siemensTokenHandler = new SiemensTokenHandler();
                var token = siemensTokenHandler.CreateAccessToken();

                return Ok(token);
               
            }
            else
            {
                return NotFound();
            }
        }
    }
}
