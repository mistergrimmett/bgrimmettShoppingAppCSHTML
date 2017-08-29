using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace bgrimmettShoppingAppCSHTML.Models
{
    public class Universal : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.FullName = user.FullName;
                ViewBag.CartItems = db.CartItems.AsNoTracking().Where(c => c.CustomerId == user.Id).ToList();
                ViewBag.TotalCartItems = user.CartItems.Sum(c => c.Count);

                decimal Total = 0;
                foreach (var item in db.CartItems.AsNoTracking().Where(c => c.CustomerId == user.Id).ToList())
                {
                    Total += item.Count * item.Item.Price;
                }
                ViewBag.CartTotal = Total;

                base.OnActionExecuted(filterContext);
            }
        }
    }
}























                                        