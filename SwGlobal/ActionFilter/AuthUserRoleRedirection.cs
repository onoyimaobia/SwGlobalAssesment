using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwGlobal.ActionFilter
{
    public class AuthUserRoleRedirection : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                bool doRedirect = true;
                string no_redirect = filterContext.HttpContext.Request.QueryString["no_redirect"];
                if (!string.IsNullOrEmpty(no_redirect))
                {
                    if (no_redirect == "true")
                    {
                        doRedirect = false;
                    }
                }
                if (doRedirect)
                {
                    string _controller = "Customer";
                    if (filterContext.HttpContext.User.IsInRole("Admin"))
                    {
                        _controller = "Admin";
                    }
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        Area = _controller,
                        action = "Index",
                        controller = "Dashboard"
                    }));
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}