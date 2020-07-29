using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.AccountDsl
{
    public class AccountDsl
    {
        //private async Task saveRefreshToken(IdentityUser user, string newRefreshToken)
        //{
        //    await _userManager.SetAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken", newRefreshToken);
        //}

        //private async Task deleteRefreshToken(IdentityUser user)
        //{
        //    await _userManager.RemoveAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken");
        //}

        //private async Task<string> GetRefreshToken(IdentityUser user)
        //{
        //    return await _userManager.GetAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken");
        //}

        //private string GenerateJwt(IEnumerable<Claim> claims)
        //{
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Authentication:JwtExpireDays"]));

        //    var token = new JwtSecurityToken(
        //        _configuration["Authentication:JwtIssuer"],
        //        _configuration["Authentication:JwtIssuer"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(5),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);

        //}

        //public string GenerateRefreshToken()
        //{
        //    byte[] randomNumber = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        return Convert.ToBase64String(randomNumber);
        //    }
        //}

        //private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //    TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the server key used to sign the JWT token is here, use more than 16 chars")),
        //        ValidateLifetime = false
        //    };

        //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;
        //    ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        //    JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

        //    //check that the algorithm used to sign key is valid
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
        //        throw new SecurityTokenException("Invalid Token");

        //    return principal;

        //}
    }
}
