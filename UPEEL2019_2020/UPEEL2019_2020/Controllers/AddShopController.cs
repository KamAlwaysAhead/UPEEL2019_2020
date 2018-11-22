using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpExcise.Models;
using UpExcise.Repository;

namespace UEEL2019_20.Controllers
{
    public class AddShopController : Controller
    {
        IShopDetails objShop;
        public AddShopController()
        {
            objShop = new ShopDetails();
            //objdal = new Repository.RandamizationNew();
        }

        // GET: AddShop
        public ActionResult AddShop()
        {
          //  TempData["dist"] = ViewBag.DisLst = objShop.DllDistrict().Where(a => a.Value == UserSession.LoggedInUserDistictId.ToString());
            TempData["dist"] = ViewBag.DisLst = objShop.DllDistrict().Where(a => a.Value == "163");
            //int flag = objShop.CheckShopDSCStatus(UserSession.LoggedInUserDistictId.ToString());
            int flag = objShop.CheckShopDSCStatus("163");
            if (flag > 0)
                ViewBag.Dsc = "Done";
            // ViewBag.DistCode = UserSession.LoggedInUserDistictId;
            return View();
        }
        [HttpGet]
        //public ActionResult GetShops()
        //{
        //    shopDetails obj = new shopDetails();
        //    obj.distCode = 0;
        //    //IEnumerable<shopDetails> products = objShop.ShopList(obj, UserSession.LoggedInUserDistictId.ToString());
        //    IEnumerable<shopDetails> products = objShop.ShopList(obj, "163");
        //    var Shop = products.OrderBy(a => a.ShopNameH).ToList();
        //    return Json(new { data = Shop }, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetShops()
        {
            shopDetails obj = new shopDetails();
            obj.distCode = Convert.ToInt32(TempData.Peek("distcode"));
            obj.ShopType = Convert.ToString(TempData.Peek("shopType"));
            //IEnumerable<shopDetails> products = objShop.ShopList(obj, UserSession.LoggedInUserDistictId.ToString());
            IEnumerable<shopDetails> products = objShop.ShopList(obj, "163");
            ViewBag.GetTehsilName = objShop.GetTehsilNameByDistrict(obj);
            ViewBag.GetThanaName = objShop.GetThanaNameByDistrict(obj);
            var Shop = products.OrderBy(a => a.ShopNameH).ToList();
            return Json(new { data = Shop }, JsonRequestBehavior.AllowGet);
        }
        public string setDistAndShoptype(string distcode, string shopType, string distName, string shopName)
        {
            TempData["distName"] = distName;
            TempData["shopName"] = shopName;
            TempData["distcode"] = distcode;
            TempData["shopType"] = shopType;
            return "true";
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            shopDetails obj = new shopDetails();
            obj.distCode = Convert.ToInt32(TempData.Peek("distcode"));
            obj.ShopType = Convert.ToString(TempData.Peek("shopType"));
            ViewBag.distName = TempData.Peek("distName").ToString();
            ViewBag.shopName = TempData.Peek("shopName").ToString();
            ViewBag.GetTehsilName = objShop.GetTehsilNameByDistrict(obj);
            ViewBag.GetThanaName = objShop.GetThanaNameByDistrict(obj);
            ViewBag.ShopTYPE = TempData.Peek("shopType").ToString();
            //var Shop = objShop.ShopList(obj, UserSession.LoggedInUserDistictId.ToString()).Where(a => Convert.ToInt32(a.ShopID) == id).FirstOrDefault();
            var Shop = objShop.ShopList(obj, "163").Where(a => Convert.ToInt32(a.ShopID) == id).FirstOrDefault();

            return View(Shop);

        }

        [HttpPost]
        public ActionResult Save(shopDetails emp)
        {
            ViewBag.DisLst = TempData["dist"];
            emp.distCode = Convert.ToInt32(TempData.Peek("distcode"));
            emp.ShopType = TempData.Peek("shopType").ToString();
            ViewBag.ShopTYPE = TempData.Peek("shopType").ToString();
            TempData["TehsilCode"] = emp.TehsilCodeCensus;
            emp.ShopNameE = emp.ShopNameE.ToUpper();
            bool status = false;
            if (ModelState.IsValid)
            {
                if (Convert.ToInt32(emp.ShopID) > 0)
                {
                    objShop.UpdateShop(emp);
                }
                else
                {
                    objShop.SaveShop(emp);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult SaveApplicant(int id)
        {
            BOShopWiseApplicant obj = new BOShopWiseApplicant();
            obj.DistrictCode = Convert.ToInt32(TempData.Peek("distcode"));
            obj.Shoptype = Convert.ToString(TempData.Peek("shopType"));
            obj.Shopid = id.ToString();
            ViewBag.distName = TempData.Peek("distName").ToString();
            ViewBag.shopName = TempData.Peek("shopName").ToString(); 
            ViewBag.ShopTYPE = TempData.Peek("shopType").ToString();
            var App = objShop.ShopWiseApplincatDetails(obj).FirstOrDefault();

            return View(App);

        }
    }
}