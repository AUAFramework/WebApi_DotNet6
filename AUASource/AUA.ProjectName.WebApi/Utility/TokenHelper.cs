using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AUA.ProjectName.Common.Tools.Config.JsonSetting;
using Microsoft.IdentityModel.Tokens;

namespace AUA.ProjectName.WebApi.Utility
{
    public class TokenHelper
    {
        public static string? GetJsonToken(string token)
        {
            var claims = GetPrincipal(token).Claims;


            return claims
                    .FirstOrDefault(p => p.Type == ClaimTypes.UserData)?
                    .Value;

        }

        private static ClaimsPrincipal? GetPrincipal(string jwtToken)
        {
            var validationParameters = GetValidationParameters();

            try
            {

                return new JwtSecurityTokenHandler()
                               .ValidateToken(jwtToken, validationParameters, out _);
            }
            catch
            {
                return null;
            }

        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = AccessTokenSetting.Issuer,
                ValidAudience = AccessTokenSetting.Audience,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(AccessTokenSetting.AccessTokenExpirationMinutes))
            };
        }

        public static bool IsExpiredToken(string jwtToken)
        {
            var principal = GetPrincipal(jwtToken);

            if (principal == null)
                return true;

            var expClaim = principal.Claims.First(x => x.Type == "exp")?.Value;


            if (string.IsNullOrWhiteSpace(expClaim) || !IsDigit(expClaim))
                return true;


            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expClaim));

            var tokenTime = dateTimeOffset.UtcDateTime;

            var minutesRemain = CalcMinutesRemain(tokenTime);

            var expirationMinutes = double.Parse(AccessTokenSetting.AccessTokenExpirationMinutes);

           
            return minutesRemain > expirationMinutes;
        }

        private static double CalcMinutesRemain(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            return timeSpan.TotalMinutes;
        }

        private static bool IsDigit(string value)
        {
            return long.TryParse(value, out _);
        }


    }
}
