using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class TokenGenerator
    {
        public static string GenerateTokenJWT(string secretKey, string securityAlgorithm, IEnumerable<Claim> claims, double LifeMinutes, string issuer, string audience)
        {
            // CREAMOS EL HEADER //
            try
            {

                var symmetricSecurityKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey)
                    );

                var signingCredentials = new SigningCredentials(
                        symmetricSecurityKey, securityAlgorithm
                    );

                var header = new JwtHeader(signingCredentials);


                // CREAMOS EL PAYLOAD //
                var payload = new JwtPayload(
                        issuer: issuer,
                        audience: audience,
                        claims: claims,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddMinutes(LifeMinutes)
                    );

                // GENERAMOS EL TOKEN //
                var token = new JwtSecurityToken(
                        header,
                        payload
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string renovarTokenJWT(string token, string secretKey, double LifeMinutes, string issuer, string audience)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken st = tokenHandler.ReadJwtToken(token);

                // CREAMOS EL HEADER //
                var symmetricSecurityKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey)
                    );
                var signingCredentials = new SigningCredentials(
                        symmetricSecurityKey, st.Header.Alg
                    );
                var header = new JwtHeader(signingCredentials);

                List<Claim> claims = new List<Claim>();

                foreach (KeyValuePair<string, Object> c in st.Payload.Where(c => c.Key == ClaimTypes.Role || c.Key == ClaimTypes.NameIdentifier ||
                                                                                 c.Key == ClaimTypes.Email || c.Key == ClaimTypes.GivenName ||
                                                                                 c.Key == ClaimTypes.Surname || c.Key == ClaimTypes.AuthenticationMethod))
                {
                    claims.Add(new Claim(c.Key, c.Value.ToString()));
                }


                // CREAMOS EL PAYLOAD //
                var payload = new JwtPayload(
                        issuer: issuer,
                        audience: audience,
                        claims: claims,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddMinutes(LifeMinutes)
                    );

                // GENERAMOS EL TOKEN //
                var newToken = new JwtSecurityToken(
                        header,
                        payload
                    );

                return new JwtSecurityTokenHandler().WriteToken(newToken);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }


}
