using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpExcise.Models
{
    public class BOShopWiseApplicant
    {
        public string Shopid { get; set; }
        public string Shoptype { get; set; }
        public string ShopName { get; set; }
        public int DistrictCode { get; set; }
        public string DistrictName{ get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAadhar { get; set; }
        public string ApplicantPAN { get; set; }
        public string CoApplicantName { get; set; }
        public string CoApplicantAadhar { get; set; }
        public string CoApplicantPAN { get; set; }

    }
}