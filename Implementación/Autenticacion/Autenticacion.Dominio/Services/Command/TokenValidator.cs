using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
	public class TokenValidator
	{
		public static bool ValidarTokenJWT(string token, string secretKey, string issuer, string audience, TimeSpan clockSkew)
		{
			var SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

			var tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = SecurityKey,
					ClockSkew = clockSkew
				}, out SecurityToken validatedToken);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		public static string extraerFirmaJWT(string token)
		{
			try
			{
				string[] parts = token.Split('.');
				string signature = parts[2];

				return signature;
			}
			catch (Exception e)
			{
				throw e;
			}
		}


		public static bool validarFirmaJWT(string token, string codeSignature)
		{
			try
			{
				string tokenSignature = extraerFirmaJWT(token);

				if (tokenSignature.CompareTo(codeSignature) == 0)
				{
					return true;
				}

				return false;

			}
			catch (Exception e)
			{
				throw e;
			}
		}


	}
}
