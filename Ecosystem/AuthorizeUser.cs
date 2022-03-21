using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ecosystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosystem
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeAttribute(params string[] claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var t = new JsonResult(new { pippo = "ciccio" });

            var user = (userprofile)context.HttpContext.Items["User"];
            var profile = context.HttpContext.Items["profile"];
            if (user == null || profile.ToString() != _claim?.First()?.ToString())
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}