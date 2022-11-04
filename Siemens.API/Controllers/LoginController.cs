using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Siemens.API.Models.Auth;
using Siemens.BLL.Service;
using Siemens.DAL.ORM.Entity.WebUser;
using Siemens.Dto.Models.WebUser.Request;
using Siemens.Dto.Models.WebUser.Response;

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
        [Route("register")]
        public IActionResult Register(WebUserRegisterRequestDto model)
        {

            WebUser webUser = new WebUser();
            webUser.EMail = model.EMail;
            webUser.Password = model.Password;

            var siemensTokenHandler = new SiemensTokenHandler();
            var token = siemensTokenHandler.CreateAccessToken(model.EMail);
            webUser.RefreshToken = token.RefreshToken;
            webUser.RefreshTokenEndDate = token.ExpirationDate.AddMinutes(5);
            token.RefreshTokenEndDate = webUser.RefreshTokenEndDate;


            var newWebUser = _unitOfWork.WebUserRepository.Add(webUser);

            if(newWebUser == null)
            {
                return StatusCode(422, "Böyle bir kullanıcı mevcut!");
            }


            _unitOfWork.Commit();

            WebUserRegisterResponseDto webUserRegisterResponseDto = new WebUserRegisterResponseDto();
            webUserRegisterResponseDto.EMail = model.EMail;
            webUserRegisterResponseDto.Id = webUser.Id;
            webUserRegisterResponseDto.AddDate = webUser.AddDate;
            webUserRegisterResponseDto.Token = token;

            return Ok(webUserRegisterResponseDto);


        }


        [HttpPost]
        public IActionResult Index(WebUserLoginRequestDto webUserLoginRequestDto)
        {
            var userControl = _unitOfWork.WebUserRepository.Any(q => q.EMail.ToLower() == webUserLoginRequestDto.EMail && q.Password == webUserLoginRequestDto.Password);

            if (userControl)
            {
                //token üretip arkadaşa göndereceğim!
                var siemensTokenHandler = new SiemensTokenHandler();
                var token = siemensTokenHandler.CreateAccessToken(webUserLoginRequestDto.EMail);

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
                    var token = siemensTokenHandler.CreateAccessToken(webUser.EMail);

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
