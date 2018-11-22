using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Web.Mvc;
using UpExcise.Models;

namespace UpExcise.Repository
{
    public interface IShopDetails
    {
        IEnumerable<Models.shopDetails> ShopList(Models.shopDetails emp, string districtCode);
        IEnumerable<Models.shopDetails> ShopListbyID(int id);
        string UpdateShop(Models.shopDetails emp);
        string SaveShop(Models.shopDetails emp);
        List<SelectListItem> DllDistrict();

        List<SelectListItem> GetTehsilNameByDistrict(Models.shopDetails emp);
        List<SelectListItem> GetThanaNameByDistrict(Models.shopDetails emp);

        bool DeleteShop(Models.shopDetails obj);

        IEnumerable<Models.shopDetails> GetShopDetails(Models.shopDetails obj);

        int CheckShopDSCStatus(string districtCode);

       // IEnumerable<Models.AppliedApplicantShop> GetAppliedAllotmentShopList();

        DataTable GetShopListForAllotment( string districtCode);

        bool DeleteApplicationAllotmentHistory(long RegistrationCode);

        //string InsertRegistrationAllotmentHistory(DataTable objRegAllotHistory);

        IEnumerable<Models.BOShopWiseApplicant> ShopWiseApplincatDetails(Models.BOShopWiseApplicant obj);

    }


}
