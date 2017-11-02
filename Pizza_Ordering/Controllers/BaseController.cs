using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    public class BaseController : ApiController
    {
        public long UserId
        {
            get
            {
                return User.Identity.GetUserId<long>();
            }
        }

        public bool UserIsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }
    }
}