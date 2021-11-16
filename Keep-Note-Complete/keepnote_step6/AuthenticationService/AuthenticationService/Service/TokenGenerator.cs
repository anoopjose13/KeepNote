using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Service
{
    public class TokenGenerator :ITokenGenerator
    {
        public string GetJWTToken(string userId)
        {
            
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_mongoapi_jwt"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "AuthServer",
                audience: "Noteapi",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds
            );
           
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
           
            return JsonConvert.SerializeObject(response);
        }
    }
}
