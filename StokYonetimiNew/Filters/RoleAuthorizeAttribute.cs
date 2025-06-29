using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Filters
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly UserRole[] _allowed;
        public RoleAuthorizeAttribute(params UserRole[] allowed)
            => _allowed = allowed;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var roleStr = context.HttpContext.Session.GetString("UserRole");
            if (!Enum.TryParse<UserRole>(roleStr, out var role)
                || !_allowed.Contains(role))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
