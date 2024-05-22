using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using WebAPISample.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebAPISample.Controllers
{
    /// <summary>
    /// Added summary
    /// </summary>
    public class MonthlyReportController : ApiController
    {
        [Route("api/MonthlyReport")]
        public IHttpActionResult Get(InputModel.Criteria objModel)
        {
            DataSet dsEmployee = new DataSet();
            DataTable dtResult = new DataTable("Summary");
            int Year = DateTime.Now.Year;
            if (objModel != null)
            {
                Year = objModel.YearIndex;
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
            {
                SqlCommand objSqlCommand = new SqlCommand("usp_GET_Monthly_Report_Year", con);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                try
                {
                    objSqlCommand.Parameters.Add("@YearIN", SqlDbType.Int);
                    objSqlCommand.Parameters["@YearIN"].Value = Year;
                    objSqlDataAdapter.Fill(dsEmployee);

                    if (dsEmployee != null && dsEmployee.Tables.Count > 0)
                    {
                        dtResult = dsEmployee.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return Ok(dtResult);
        }
        //[Route("api/MonthlyReport")]
        //public IHttpActionResult Post(InputModel.Criteria objModel)
        //{
        //    DataSet dsEmployee = new DataSet();
        //    DataTable dtResult = new DataTable("Summary");
        //    int Year = DateTime.Now.Year;
        //    if (objModel != null)
        //    {
        //        Year = objModel.YearIndex;
        //    }

        //    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
        //    {
        //        SqlCommand objSqlCommand = new SqlCommand("usp_GET_Monthly_Report_Year", con);
        //        objSqlCommand.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
        //        try
        //        {
        //            objSqlCommand.Parameters.Add("@YearIN", SqlDbType.Int);
        //            objSqlCommand.Parameters["@YearIN"].Value = Year;
        //            objSqlDataAdapter.Fill(dsEmployee);

        //            if (dsEmployee != null && dsEmployee.Tables.Count > 0)
        //            {
        //                dtResult = dsEmployee.Tables[0];
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }

        //    return Ok(dsEmployee);
        //}

        //[Route("api/MonthlyReport")]
        //public IHttpActionResult Post(InputModel.CriteriaSave objModel)
        //{
        //    int RowsAffected = 0;
        //    if (objModel != null)
        //    {
        //        using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
        //        {
        //            SqlCommand objSqlCommand = new SqlCommand("usp_SAVE_Monthly_Expense", con);
        //            objSqlCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
        //            try
        //            {
        //                objSqlCommand.Parameters.Add("@TranDate", SqlDbType.DateTime);
        //                objSqlCommand.Parameters["@TranDate"].Value = objModel.TranDate;

        //                objSqlCommand.Parameters.Add("@BankDesc", SqlDbType.NVarChar);
        //                objSqlCommand.Parameters["@BankDesc"].Value = objModel.BankDesc;

        //                objSqlCommand.Parameters.Add("@BankDesc2", SqlDbType.NVarChar);
        //                objSqlCommand.Parameters["@BankDesc2"].Value = objModel.BankDesc2;

        //                objSqlCommand.Parameters.Add("@MyDesc", SqlDbType.NVarChar);
        //                objSqlCommand.Parameters["@MyDesc"].Value = objModel.MyDesc;

        //                objSqlCommand.Parameters.Add("@AmtDeducted", SqlDbType.NVarChar);
        //                objSqlCommand.Parameters["@AmtDeducted"].Value = objModel.AmtDeducted;

        //                objSqlCommand.Parameters.Add("@PaidUsing", SqlDbType.NVarChar);
        //                objSqlCommand.Parameters["@PaidUsing"].Value = objModel.PaidUsing;

        //                objSqlCommand.Parameters.Add("@IsInvestment", SqlDbType.Int);
        //                objSqlCommand.Parameters["@IsInvestment"].Value = objModel.IsInvestment;

        //                objSqlCommand.Parameters.Add("@FYYear", SqlDbType.Int);
        //                objSqlCommand.Parameters["@FYYear"].Value = objModel.FYYear;

        //                con.Open();
        //                RowsAffected = objSqlCommand.ExecuteNonQuery();
        //                con.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //    }
        //    return Ok(RowsAffected);
        //}


        [Route("api/MonthlyReport")]
        public IHttpActionResult Post(InputModel.ObjCriteria objModel)
        {
            object objReturn = null;
            DataTable dtResult = new DataTable("dtSave");
            int RowsAffected = 0;
            if (objModel != null)
            {
                if (objModel.CrudType == "0")
                {
                    DataSet dsEmployee = new DataSet();
                    dtResult = objModel.dtSave;

                    int Year = DateTime.Now.Year;
                    if (dtResult != null)
                    {
                        Year = Convert.ToInt32(dtResult.Rows[0]["YearIndex"].ToString());
                    }

                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
                    {
                        SqlCommand objSqlCommand = new SqlCommand("usp_GET_Monthly_Report_Year", con);
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                        try
                        {
                            objSqlCommand.Parameters.Add("@YearIN", SqlDbType.Int);
                            objSqlCommand.Parameters["@YearIN"].Value = Year;
                            objSqlDataAdapter.Fill(dsEmployee);

                            if (dsEmployee != null && dsEmployee.Tables.Count > 0)
                            {
                                objReturn = dsEmployee;

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (objModel.CrudType == "1")
                {
                    //IList<InputModel.CriteriaSave> items = objModel.dtSave.AsEnumerable().Select(row =>
                    //                                        new InputModel.CriteriaSave
                    //                                        {
                    //                                            TranDate = row.Field<string>("TranDate"),
                    //                                            BankDesc = row.Field<string>("BankDesc"),
                    //                                            BankDesc2 = row.Field<string>("BankDesc2"),
                    //                                            MyDesc = row.Field<string>("MyDesc"),
                    //                                            AmtDeducted = Convert.ToDouble(row.Field<double>("AmtDeducted")),
                    //                                            PaidUsing = row.Field<string>("PaidUsing"),
                    //                                            IsInvestment = row.Field<Boolean>("IsInvestment"),
                    //                                            FYYear = Convert.ToInt32(row.Field<Int64>("FYYear"))
                    //                                        }).ToList();
                    dtResult = objModel.dtSave;
                    if (dtResult != null)
                    {
                        using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
                        {
                            SqlCommand objSqlCommand = new SqlCommand("usp_SAVE_Monthly_Expense", con);
                            objSqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                            try
                            {
                                objSqlCommand.Parameters.Add("@TranDate", SqlDbType.DateTime);
                                objSqlCommand.Parameters["@TranDate"].Value = dtResult.Rows[0]["TranDate"];

                                objSqlCommand.Parameters.Add("@BankDesc", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@BankDesc"].Value = dtResult.Rows[0]["BankDesc"];

                                objSqlCommand.Parameters.Add("@BankDesc2", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@BankDesc2"].Value = dtResult.Rows[0]["BankDesc2"];

                                objSqlCommand.Parameters.Add("@MyDesc", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@MyDesc"].Value = dtResult.Rows[0]["MyDesc"];

                                objSqlCommand.Parameters.Add("@AmtDeducted", SqlDbType.Decimal);
                                objSqlCommand.Parameters["@AmtDeducted"].Value = dtResult.Rows[0]["AmtDeducted"];

                                objSqlCommand.Parameters.Add("@PaidUsing", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@PaidUsing"].Value = dtResult.Rows[0]["PaidUsing"];

                                objSqlCommand.Parameters.Add("@IsInvestment", SqlDbType.Int);
                                objSqlCommand.Parameters["@IsInvestment"].Value = (dtResult.Rows[0]["IsInvestment"].ToString() == "" ||
                                    dtResult.Rows[0]["IsInvestment"].ToString().ToLower() == "false" ||
                                    dtResult.Rows[0]["IsInvestment"].ToString() == "0") ? "0" : "1";

                                objSqlCommand.Parameters.Add("@FYYear", SqlDbType.Int);
                                objSqlCommand.Parameters["@FYYear"].Value = dtResult.Rows[0]["FYYear"];

                                con.Open();
                                objReturn = objSqlCommand.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                else if (objModel.CrudType == "2")
                {
                    dtResult = objModel.dtSave;
                    if (dtResult != null)
                    {
                        using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
                        {
                            SqlCommand objSqlCommand = new SqlCommand("usp_SAVE_Petrol_Expense", con);
                            objSqlCommand.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                            try
                            {
                                objSqlCommand.Parameters.Add("@TranType", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@TranType"].Value = dtResult.Rows[0]["TranType"];

                                objSqlCommand.Parameters.Add("@TranDate", SqlDbType.DateTime);
                                objSqlCommand.Parameters["@TranDate"].Value = dtResult.Rows[0]["TranDate"];

                                objSqlCommand.Parameters.Add("@BankDesc", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@BankDesc"].Value = dtResult.Rows[0]["BankDesc"];

                                objSqlCommand.Parameters.Add("@MyDesc", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@MyDesc"].Value = dtResult.Rows[0]["MyDesc"];

                                objSqlCommand.Parameters.Add("@AmtDeducted", SqlDbType.Decimal);
                                objSqlCommand.Parameters["@AmtDeducted"].Value = dtResult.Rows[0]["AmtDeducted"];

                                objSqlCommand.Parameters.Add("@PaidUsing", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@PaidUsing"].Value = dtResult.Rows[0]["PaidUsing"];

                                objSqlCommand.Parameters.Add("@TotalLitre", SqlDbType.Decimal);
                                objSqlCommand.Parameters["@TotalLitre"].Value = dtResult.Rows[0]["TotalLitre"];

                                objSqlCommand.Parameters.Add("@MyLocation", SqlDbType.NVarChar);
                                objSqlCommand.Parameters["@MyLocation"].Value = dtResult.Rows[0]["MyLocation"].ToString();

                                objSqlCommand.Parameters.Add("@FYYear", SqlDbType.Int);
                                objSqlCommand.Parameters["@FYYear"].Value = dtResult.Rows[0]["FYYear"];

                                objSqlCommand.Parameters.Add("@KMDriven", SqlDbType.Int);
                                objSqlCommand.Parameters["@KMDriven"].Value = dtResult.Rows[0]["KMDriven"];

                                con.Open();
                                objReturn = objSqlCommand.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
            }

            return Ok(objReturn);
        }
    }
}