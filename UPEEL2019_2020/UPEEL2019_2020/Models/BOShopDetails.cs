using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpExcise.Models
{
    public class shopDetails
    {
        [Required(ErrorMessage = "Enter Shop Name English")]
        //^[a-zA-Z0-9\s.\,\)\(\-\_]+$
        // ^[ A-Za-z0-9,_-]*$
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Shop Name must be between 4 to 100 Char")]
        [RegularExpression(@"^([ a-zA-Z0-9_\.\-\)\(])+$", ErrorMessage = "Enter Shop Name in English and (-,_)( are allowed)")]
        public string ShopNameE { get; set; }
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Shop Name must be between 4 to 100 Char")]
        [Required(ErrorMessage = "Enter Shop Name Hindi")]
        [RegularExpression(@"^[^A-Za-z;@#$%^&[\/\]~*\}\{]*$", ErrorMessage = "Enter Shop Name Only in Hindi and (-,_)( are allowed)")]
        public string ShopNameH { get; set; }
        public string RowNum { get; set; }
        public string ShopDescription { get; set; }
        public string ShopType { get; set; }
        public string DisttName { get; set; }
        public string ApplicantName { get; set; }
        public string approvedBy { get; set; }
        public string aaplicationCode { get; set; }
        public string ShopID { get; set; }
        public Int32 distCode { get; set; }
        public string shopSelected { get; set; }
        public string Total { get; set; }
        public string LoginID { get; set; }
        public string ShopAddress { get; set; }
        public string ApplicantCode { get; set; }
        public string ApplicationTitle { get; set; }
        public string RoundCode { get; set; }
        public string ApplicationPublishCode { get; set; }
        public string ApplicationType { get; set; }
        ////////////////////////////

        public string TehsilCodeCensus { get; set; }
        public string TehsilNameEnglish { get; set; }
        public string TehsilNameHindi { get; set; }
        public string Tcode { get; set; }
        public string tname { get; set; }
        public string tnameHindi { get; set; }
        /////////////////////////////////

        public string CircleType { get; set; }
        public string SectorType { get; set; }
        public string AreaType { get; set; }
        ////////////////////////////////////

        [Required(ErrorMessage = "Enter Valid Amount")]
        [RegularExpression("^[0-9]*[0-9]+[.]?[0-9]*$")]
        public string MGQ_Basic_Fees { get; set; }


        public string BLFees { get; set; }
        public string LicenseFees { get; set; }
        public string EarnestMoney { get; set; }
        public string SolvencyRequired { get; set; }

        public string DistrictCode { get; set; }

        public string TotalApplicant { get; set; }
        //  [Range(1, 999, ErrorMessage = ("Enter Valid Number (Between 1-999)"))]
        public int SeedNo { get; set; }

        public string ApplicationCode { get; set; }

        public byte[] ApplicantPhoto { get; set; }


        public string ApplicantImg { get; set; }
        public string ApplicationDate { get; set; }
        public string RegistrationCode { get; set; }
        public string AllotmentDate { get; set; }

        public string TehsilName { get; set; }
        public string ThanaName { get; set; }

        ///////////////////

        public string Random_No { get; set; }
        public string Mock { get; set; }
        public string MockFlag { get; set; }
        public string UserIP { get; set; }
        public string UpdatedBy { get; set; }
        [Required(ErrorMessage = "Enter Valid Number")]
        [Range(1, 5)]
        public string RandamizationTimes { get; set; }
        public string RunningMock { get; set; }
        public string RemainingMock { get; set; }
        public string Mobile { get; set; }
        public string Sr { get; set; }
        public string RandomMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string SApplicantName { get; set; }
        public string subapplicantname { get; set; }

        public string TotalFLWithNoApplicantsShops { get; set; }
        public string TotalBEWithNoApplicantsShops { get; set; }
        public string TotalMSWithNoApplicantsShops { get; set; }
        public string TotalCLWithNoApplicantsShops { get; set; }

        public string TotalCLShops { get; set; }
        public string TotalCLApprovedCandidates { get; set; }

        public string TotalFLShops { get; set; }
        public string TotalFLApprovedCandidates { get; set; }

        public string TotalBEShops { get; set; }
        public string TotalBEApprovedCandidates { get; set; }

        public string TotalMSShops { get; set; }
        public string TotalMSApprovedCandidates { get; set; }
        public string ctimestamp { get; set; }
        //public string Total_Applicant { get; set; }

        public string rts { get; set; }
    }
    public class MockData
    {
        public string RowNum { get; set; }
        public string ShopNameE { get; set; }
        public string Total_Applicant { get; set; }
        public string Mock_1_Application_code { get; set; }
        public string Mock_1_ApplicantName { get; set; }
        public string Mock_2_Application_code { get; set; }
        public string Mock_2_ApplicantName { get; set; }
        public string Mock_3_Application_code { get; set; }
        public string Mock_3_ApplicantName { get; set; }
        public string Mock_4_Application_code { get; set; }
        public string Mock_4_ApplicantName { get; set; }
        public string Mock_5_Application_code { get; set; }
        public string Mock_5_ApplicantName { get; set; }
        public string Final_Application_code { get; set; }
        public string Final_ApplicantName { get; set; }


        public string disttCode { get; set; }
        public string ShopType { get; set; }

       
    }
    public class BODashboardRandomization
    {
        public string RowNum { get; set; }
        public string distCode { get; set; }
        public string DistName { get; set; }
        public string Bulb_Color { get; set; }
    }
}