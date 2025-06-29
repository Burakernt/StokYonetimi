using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StokYonetimiNew.Models;
using System;
using System.Linq;

namespace StokYonetimiNew.Filters
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        public UserRole[] Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 1) Eğer [AllowAnonymous] varsa, filtreyi atla:
            var hasAllowAnonymous = context
                .ActionDescriptor
                .EndpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();
            if (hasAllowAnonymous)
                return;

            var session = context.HttpContext.Session;
            var userId = session.GetInt32("UserId");
            if (userId == null)
            {
                // Login değilse Account/Login sayfasına yönlendir
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            // 2) Rol kısıtlaması varsa kontrol et
            if (Roles != null && Roles.Length > 0)
            {
                var roleStr = session.GetString("UserRole");
                if (!Enum.TryParse<UserRole>(roleStr, out var role)
                    || !Roles.Contains(role))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
