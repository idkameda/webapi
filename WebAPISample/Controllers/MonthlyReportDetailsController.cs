using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPISample.Models;
using System.Configuration;
using System.Web;

namespace WebAPISample.Controllers
{
    [Route("api/MonthlyReportDetails")]
    public class MonthlyReportDetailsController : ApiController
    {
        public IHttpActionResult Post(InputModel.Criteria objModel)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            DataSet dsEmployee = new DataSet();
            DataTable dtResult = new DataTable("Table3");
            object objReturn = null;
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;

            if (objModel != null)
            {
                if (objModel.CrudType == "0")
                {
                    Year = objModel.YearIndex;
                    Month = objModel.MonthIndex;

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        SqlCommand objSqlCommand = new SqlCommand("[dbo].[usp_GET_Monthly_Report_Details]", con);
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                        try
                        {
                            objSqlCommand.Parameters.Add("@YearIN", SqlDbType.Int);
                            objSqlCommand.Parameters["@YearIN"].Value = Year;
                            objSqlCommand.Parameters.Add("@MonthIn", SqlDbType.Int);
                            objSqlCommand.Parameters["@MonthIn"].Value = Month;
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
                else if (objModel.CrudType != "0")
                {
                    Year = objModel.YearIndex;
                    string strRepType = string.Empty;
                    switch (objModel.CrudType)
                    {
                        case "1":
                            strRepType = "Transaction";
                            break;
                        case "2":
                            strRepType = "Investment";
                            break;
                        case "3":
                            strRepType = "Petrol";
                            break;
                        case "4":
                            strRepType = "Diesel";
                            break;
                    }

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        SqlCommand objSqlCommand = new SqlCommand("[dbo].[usp_GET_Monthly_Report_RecID]", con);
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                        try
                        {
                            objSqlCommand.Parameters.Add("@ReportType", SqlDbType.NVarChar);
                            objSqlCommand.Parameters["@ReportType"].Value = strRepType;
                            objSqlCommand.Parameters.Add("@RecID", SqlDbType.Int);
                            objSqlCommand.Parameters["@RecID"].Value = Year;
                            objSqlDataAdapter.Fill(dtResult);

                            if (dtResult != null)
                            {
                                objReturn = dtResult;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            return Ok(objReturn);
        }

        public IHttpActionResult Get(string SearchText)
        {
            object objReturn = null;
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            DataTable dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand objSqlCommand = new SqlCommand("[dbo].[usp_SELECT_MyDesc]", con);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                try
                {
                    objSqlCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar);
                    objSqlCommand.Parameters["@SearchText"].Value = SearchText.Trim();
                    objSqlDataAdapter.Fill(dtResult);

                    if (dtResult != null)
                    {
                        objReturn = dtResult;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return Ok(objReturn);
        }
    }
}
