using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Yad2Project.ViewModel
{
    public static class CookieHelper
    {
        public static string GetUserBycookie(HttpCookie authCookie)
        {
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket.Name;
            }
            return null;
        }
    }
}