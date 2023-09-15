using SbBlog.API.Models.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SbBlog.API.Models.Entities
{
    public class JWTService
    {  
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _jwtkey;
        public JWTService(IConfiguration config)
        {
            _config = config;
            _jwtkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }

           public string CreateJWT(User user)
          {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
               //new Claim("my own claim name", "this is the value"), //this was for demo purpose
           };



            // on Postman test as  post request-----  http://localhost:5197/api/account/login
            // add at body-raw-json
            //            {
            //                "Username": "sbhure@example.com",
            //                "Password": "123456"
            //            }
            ////----------------we get this as async response------------
            //            {
            //                "firstName": "shraddha",
            //                "lastName": "bhure",
            //            "jwt": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJiOTZjOTAwMi1jN2MxLTQ2MjQtYmI3My02ZGI5MThmNDJiNjYiLCJlbWFpbCI6InNiaHVyZUBleGFtcGxlLmNvbSIsImdpdmVuX25hbWUiOiJzaHJhZGRoYSIsImZhbWlseV9uYW1lIjoiYmh1cmUiLCJteSBvd24gY2xhaW0gbmFtZSI6InRoaXMgaXMgdGhlIHZhbHVlIiwibmJmIjoxNjk0MDY2NjQyLCJleHAiOjE2OTUzNjI2NDEsImlhdCI6MTY5NDA2NjY0MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTk3In0.MARu0zABSfC-rV_1WJy4rg9mgZS6tTi4nPPTVeSF1y8"
            //           }
            ///------------------ test this jwt token at this jwt website-----------
            //----------- on website https://jwt.io/
            //       paste  this   "jwt": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJiOTZjOTAwMi1jN2MxLTQ2MjQtYmI3My02ZGI5MThmNDJiNjYiLCJlbWFpbCI6InNiaHVyZUBleGFtcGxlLmNvbSIsImdpdmVuX25hbWUiOiJzaHJhZGRoYSIsImZhbWlseV9uYW1lIjoiYmh1cmUiLCJteSBvd24gY2xhaW0gbmFtZSI6InRoaXMgaXMgdGhlIHZhbHVlIiwibmJmIjoxNjk0MDY2NjQyLCJleHAiOjE2OTUzNjI2NDEsImlhdCI6MTY5NDA2NjY0MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTk3In0.MARu0zABSfC-rV_1WJy4rg9mgZS6tTi4nPPTVeSF1y8"
            //        at Encoded Section
            //----------------------------We get decoaded payload data as Like this
            //
            //------HEADER:ALGORITHM & TOKEN TYPE

            //  {
            //    "alg": "HS256",
            //    "typ": "JWT"
            //  }
            ///------ PAYLOAD: DATA
            ///{
            //  "nameid": "b96c9002-c7c1-4624-bb73-6db918f42b66",
            //  "email": "sbhure@example.com",
            //  "given_name": "shraddha",
            //  "family_name": "bhure",
            //  "my own claim name": "this is the value",
            //  "nbf": 1694066642,
            //  "exp": 1695362641,
            //  "iat": 1694066642,
            //  "iss": "http://localhost:5197"
            //}



            // -----------------inside the  postman  login and refresh-user-token  Test Section add this code

            //const user = pm.response.json();
            //pm.globals.set('jwt', user.jwt);


            var creadentaials = new SigningCredentials(_jwtkey, SecurityAlgorithms.HmacSha256); ;//used for 32 bit jwt key

            // var creadentaials = new SigningCredentials(_jwtkey, SecurityAlgorithms.HmacSha512Signature);//used for 512 bit jwt key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                SigningCredentials = creadentaials,
                Issuer = _config["JWT:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwt);
                
           }
        
    }
}
