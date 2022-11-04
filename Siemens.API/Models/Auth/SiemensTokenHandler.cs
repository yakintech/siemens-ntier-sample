using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.IdentityModel.Tokens.Jwt;


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


            token.ExpirationDate = DateTime.Now.AddMinutes(5);

            //Oluşturduğum token özellikleri
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "cagatay@mail.com",
                audience: "cagatay2@mail.com",
                expires: token.ExpirationDate,
                signingCredentials: signingCredentials
                );


           //Token create ediyorum
           JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
           token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
