using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ecosystem.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecosystem.RepoService;

namespace Ecosystem
{
    public class MdlWareValidate
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        IInsertTable<userprofile> _userService;

        public MdlWareValidate(RequestDelegate next, IConfiguration configuration,IInsertTable<userprofile> userService)
        {
            _next = next;
            _configuration = configuration;
            _userService = userService;
         //   _userService = new InsertTable(configuration);
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token);

            await _next(context);
        }

        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var accountId = ((JwtSecurityToken)validatedToken).Claims.First(x => x.Type == "id").Value;
             //   var profileuser = ((JwtSecurityToken)validatedToken).Claims.First(x => x.Type == "profile").Value;
                ((JwtSecurityToken)validatedToken).Claims.ToDictionary(x => x.Type, x => x.Value).TryGetValue("profile", out string profile);

                // attach account to context on successful jwt validation
                context.Items["User"] = _userService.IsValidUserInformationFDirect(new Entity.userprofile {username= accountId });
                context.Items["profile"] = profile;
            }
            catch (Exception ex)
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}