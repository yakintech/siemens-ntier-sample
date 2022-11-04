using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Siemens.API.Models.Auth
{
    public class SiemensTokenHandler
    {

        public Token CreateAccessToken()
        {
            Token token = new Token();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ironmaidenpentagramslipknotironmaidenpentagramslipknot"));

            //Token şifreleme algoritması
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            token.ExpirationDate = DateTime.Now.AddMinutes(1);

            //Oluşturduğum token özellikleri
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "cagatay@mail.com",
                audience: "cagatay1@mail.com",
                expires: token.ExpirationDate,
                signingCredentials: signingCredentials
                );


           //Token create ediyorum
           JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
           token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
           token.RefreshToken = CreateRefreshToken();


            return token;
        }


        private string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
