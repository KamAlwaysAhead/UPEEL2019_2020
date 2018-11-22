using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpExcise.Models;
using UpExcise.Repository;

namespace UEEL2019_20.Controllers
{
    public class GetMenuController : Controller
    {
        public virtual ActionResult Menu()        {
            IEnumerable<Menu> Menu = null;
            //Menu = MenuData.GetMenus(UserSession.LoggedInUserId, UserSession.LoggedInUserRole);
            Menu = MenuData.GetMenus("1016", 3);
            return PartialView("Menu",Menu);
        }

    }
}