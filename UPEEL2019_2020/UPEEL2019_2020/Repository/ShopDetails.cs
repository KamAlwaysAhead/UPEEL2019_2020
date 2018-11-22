using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UpExcise.Models;
using Dapper;
using System.Data;
using System.Web.Mvc;

namespace UpExcise.Repository
{
    public class ShopDetails : IShopDetails
    {
        public IEnumerable<Models.shopDetails> ShopList(Models.shopDetails emp, string districtCode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 1);
                para.Add("@distcode", districtCode);
                para.Add("@shopType", emp.ShopType);
                try
                {
                    return con.Query<Models.shopDetails>("ProcShopList", para, null, true, 0, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        public IEnumerable<Models.shopDetails> ShopListbyID(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 2);
                para.Add("@Shopid", id);
                return con.Query<Models.shopDetails>("ProcShopList", para, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }


        public string UpdateShop(Models.shopDetails emp)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 3);
                para.Add("@ShopNameE", emp.ShopNameE);
                para.Add("@ShopNameH", emp.ShopNameH);
                para.Add("@ShopID", emp.ShopID);
                para.Add("@ThanaCode", emp.Tcode);
                para.Add("@TehsilCode", emp.TehsilCodeCensus);
                para.Add("@Circle", emp.CircleType);
                para.Add("@Sector", emp.SectorType);
                para.Add("@Location", emp.AreaType);
                para.Add("@shopType", emp.ShopType);
                para.Add("@MGQ_BL", emp.MGQ_Basic_Fees);
                return con.Query<Models.shopDetails>("ProcShopList", para, null, true, 0, commandType: CommandType.StoredProcedure).ToString();
            }

        }
        public int CheckShopDSCStatus(string DistrictCode)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 8);
                para.Add("@distcode", DistrictCode);





                para.Add("@pOutPut", dbType: DbType.Int32, direction: ParameterDirection.Output);

                con.Execute("ProcShopList", para, null, 0, CommandType.StoredProcedure);



                int MemID = para.Get<int>("@pOutPut");
                return MemID;
            }




        }


        public string SaveShop(Models.shopDetails emp)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 4);
                para.Add("@ShopNameE", emp.ShopNameE);
                para.Add("@ShopNameH", emp.ShopNameH);
                para.Add("@distcode", emp.distCode);
                para.Add("@shopType", emp.ShopType);
                para.Add("@ThanaCode", emp.Tcode);
                para.Add("@TehsilCode", emp.TehsilCodeCensus);
                para.Add("@Circle", emp.CircleType);
                para.Add("@Sector", emp.SectorType);
                para.Add("@Location", emp.AreaType);
                para.Add("@MGQ_BL", emp.MGQ_Basic_Fees);
                // para.AdTehsilCoded("@ShopAddress", emp.ShopAddress);

                try
                {
                    return con.Query<Models.shopDetails>("ProcShopList", para, null, true, 0, commandType: CommandType.StoredProcedure).ToString();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

        }

        public List<SelectListItem> DllDistrict()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                DataTable objDt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Master_District order by DisttName", con);
                adp.Fill(objDt);

                List<SelectListItem> objLst = new List<SelectListItem>();
                SelectListItem Items;
                Items = new SelectListItem();
                Items.Text = "--SELECT--";
                Items.Value = "-1";

                objLst.Add(Items);
                for (int i = 0; i < objDt.Rows.Count; i++)
                {
                    Items = new SelectListItem();
                    Items.Text = objDt.Rows[i]["DisttName"].ToString();
                    Items.Value = objDt.Rows[i]["Distt_Code"].ToString();

                    objLst.Add(Items);
                }
                return objLst;
            }
        }



        public List<SelectListItem> GetTehsilNameByDistrict(Models.shopDetails emp)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                DataTable objDt = new DataTable();
                SqlCommand cmd = new SqlCommand("ProcShopList", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpType", 5);
                cmd.Parameters.AddWithValue("@distcode", emp.distCode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(objDt);
                if (objDt.Rows.Count > 0)
                {
                    List<SelectListItem> objLst = new List<SelectListItem>();
                    SelectListItem Items;
                    Items = new SelectListItem();
                    Items.Text = "--SELECT--";
                    Items.Value = "0";

                    objLst.Add(Items);
                    for (int i = 0; i < objDt.Rows.Count; i++)
                    {
                        Items = new SelectListItem();
                        Items.Text = objDt.Rows[i]["TehsilNameEnglish"].ToString();
                        Items.Value = objDt.Rows[i]["TehsilCodeCensus"].ToString();

                        objLst.Add(Items);
                    }
                    return objLst;
                }

                else
                {
                    List<SelectListItem> objLst = new List<SelectListItem>();
                    SelectListItem Items;
                    Items = new SelectListItem();
                    Items.Text = "--None--";
                    Items.Value = "0";
                    objLst.Add(Items);
                    return objLst;
                }
            }


        }
        public List<SelectListItem> GetThanaNameByDistrict(Models.shopDetails emp)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                DataTable objDt = new DataTable();
                SqlCommand cmd = new SqlCommand("ProcShopList", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpType", 6);
                cmd.Parameters.AddWithValue("@distcode", emp.distCode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(objDt);
                if (objDt.Rows.Count > 0)
                {
                    List<SelectListItem> objLst = new List<SelectListItem>();
                    SelectListItem Items;
                    Items = new SelectListItem();
                    Items.Text = "--SELECT--";
                    Items.Value = "0";

                    objLst.Add(Items);
                    for (int i = 0; i < objDt.Rows.Count; i++)
                    {
                        Items = new SelectListItem();
                        Items.Text = objDt.Rows[i]["tnameHindi"].ToString();
                        Items.Value = objDt.Rows[i]["Tcode"].ToString();

                        objLst.Add(Items);
                    }
                    return objLst;
                }

                else
                {
                    List<SelectListItem> objLst = new List<SelectListItem>();
                    SelectListItem Items;
                    Items = new SelectListItem();
                    Items.Text = "--None--";
                    Items.Value = "0";
                    objLst.Add(Items);
                    return objLst;
                }
            }


        }

        public bool DeleteShop(shopDetails obj)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("ProcShopList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SpType", 7);
                    cmd.Parameters.AddWithValue("@Shopid", obj.ShopID);
                    con.Open();
                    //cmd.ExecuteNonQuery(); //Close

                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
        }


        public IEnumerable<Models.shopDetails> GetShopDetails(Models.shopDetails obj)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 1);
                para.Add("@DisttCode", obj.distCode);
                para.Add("@shopType", obj.ShopType);
                para.Add("@ThanaCode", obj.Tcode);
                para.Add("@TehsilCode", obj.TehsilCodeCensus);
                para.Add("@CircleType", obj.CircleType);
                para.Add("@SectorType", obj.SectorType);
                para.Add("@AreaType", obj.AreaType);
                try
                {
                    return con.Query<Models.shopDetails>("proc_displayShopToPublic", para, null, true, 0, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        public DataTable GetShopListForAllotment(string districtCode)
        {
            DataTable ds = new DataTable();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_ShopListForAllotment]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SpType", 1));
                cmd.Parameters.Add(new SqlParameter("@distcode", districtCode));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }


        //public string InsertRegistrationAllotmentHistory(DataTable objRegAllotHistory)
        //{
        //    string message = "Save";
        //    SqlCommand cmd;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
        //    con.Open();
        //    new DataTable();
        //    SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
        //    try
        //    {
        //        if (objRegAllotHistory != null)
        //        {
        //            if (objRegAllotHistory.Rows.Count > 0)
        //            {
        //                cmd = new SqlCommand();
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "[Proc_InsertRegistrationAllotmentHistory]";
        //                cmd.CommandTimeout = 3600;
        //                cmd.Parameters.Add(new SqlParameter("@InsertRegistrationAllotmentHistory", objRegAllotHistory));
        //                cmd.Parameters.Add(new SqlParameter("@CreatedBy", UserSession.LoggedInUserName.ToString()));
        //                cmd.Parameters.Add(new SqlParameter("@CreatedDate", null));
        //                cmd.Parameters.Add(new SqlParameter("@IP", Applicant.GetIpAddress()));
        //                cmd.Parameters.Add(new SqlParameter("Msg", ""));
        //                cmd.Parameters["Msg"].Size = 256;
        //                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
        //                cmd.Transaction = transaction;
        //                cmd.ExecuteNonQuery();
        //                message = cmd.Parameters["Msg"].Value.ToString();
        //            }
        //        }
        //        transaction.Commit();
        //    }
        //    catch (Exception ex1)
        //    {
        //        transaction.Rollback();
        //        message = ex1.Message;
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //    return message;
        //}

        //public IEnumerable<Models.AppliedApplicantShop> GetAppliedAllotmentShopList()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
        //    {
        //        var para = new DynamicParameters();
        //        para.Add("@SpType", 1);
        //        try
        //        {
        //            return con.Query<Models.AppliedApplicantShop>("Proc_GetApplicationAllotmentHistory", para, null, true, 0, commandType: CommandType.StoredProcedure);
        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }

        //    }
        //}


        public bool DeleteApplicationAllotmentHistory(long RegistrationCode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_GetApplicationAllotmentHistory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SpType", 2);
                    cmd.Parameters.AddWithValue("@RegistrationCode", RegistrationCode);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        #region ShopWiseApplicant


        ///  1. Get shopwise applicant details                  
        public IEnumerable<Models.BOShopWiseApplicant> ShopWiseApplincatDetails(Models.BOShopWiseApplicant Apl)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SpType", 9);
                para.Add("@distcode", Apl.DistrictCode);
                para.Add("@shopType", Apl.Shoptype);
                para.Add("@Shopid", Apl.Shopid);
                try
                {
                    return con.Query<Models.BOShopWiseApplicant>("ProcShopList", para, null, true, 0, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        #endregion



    }
}