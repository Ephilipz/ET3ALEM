using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Server_Application.Models.Account
{
    public class TokenHandler
    {
        public readonly IConfiguration _IConfiguration;

        public TokenHandler(IConfiguration iConfiguration)
        {
            _IConfiguration = iConfiguration;
        }

        public string GenerateJwt(IEnumerable<Claim> claims, int? duration = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Authentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _IConfiguration["Authentication:JwtIssuer"],
                _IConfiguration["Authentication:JwtIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(duration ?? 5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Authentication:JwtKey"])),
                ValidateLifetime = false
            };

            var principal = ValidateOldJWT(token, tokenValidationParameters);
            return principal;
        }

        private static ClaimsPrincipal ValidateOldJWT(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal =
                    tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                var isValidJWTHash = jwtSecurityToken?.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture);
                if (jwtSecurityToken == null || (bool) !isValidJWTHash)
                    throw new SecurityTokenException("Invalid Token");
                return principal;
            }
            catch (Exception)
            {
                throw new SecurityTokenInvalidSignatureException("Invalid token");
            }
        }
    }
}