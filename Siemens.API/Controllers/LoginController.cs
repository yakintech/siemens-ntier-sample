using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Siemens.API.Models.Auth;
using Siemens.BLL.Service;
using Siemens.DAL.ORM.Entity.WebUser;
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

                var loginUser = _unitOfWork.WebUserRepository.FirstOrDefault(q => q.EMail.ToLower() == webUserLoginRequestDto.EMail && q.Password == webUserLoginRequestDto.Password);

                loginUser.RefreshToken = token.RefreshToken;
                loginUser.RefreshTokenEndDate = token.ExpirationDate.AddMinutes(5);
                _unitOfWork.Commit();

                token.RefreshTokenEndDate = loginUser.RefreshTokenEndDate;

                return Ok(token);

            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("refreshToken")]
        public IActionResult RefreshToken(WebUserRefreshTokenRequestDto model)
        {
            WebUser webUser = _unitOfWork.WebUserRepository.FirstOrDefault(q => q.RefreshToken == model.RefreshToken);

            if (webUser != null)
            {
                if (webUser.RefreshTokenEndDate > DateTime.Now)
                {
                    var siemensTokenHandler = new SiemensTokenHandler();
                    var token = siemensTokenHandler.CreateAccessToken();

                    webUser.RefreshToken = token.RefreshToken;
                    webUser.RefreshTokenEndDate = token.ExpirationDate.AddMinutes(5);

                    token.RefreshTokenEndDate = webUser.RefreshTokenEndDate;

                    _unitOfWork.Commit();

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Zaman aşımı...!");
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
