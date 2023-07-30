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
        public IHttpActionResult Get()
        {
            DataSet dsEmployee = new DataSet();
            DataTable dtResult = new DataTable("Summary");
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KR9DDHA\SQLEXPRESS;Initial Catalog=BSE_NSE; User Id=sa; Password=P@$$;"))
            {
                SqlCommand objSqlCommand = new SqlCommand("usp_GET_Monthly_Report_Year", con);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                try
                {
                    objSqlCommand.Parameters.Add("@YearIN", SqlDbType.Int);
                    objSqlCommand.Parameters["@YearIN"].Value = 2023;
                    objSqlDataAdapter.Fill(dsEmployee);

                    if(dsEmployee!=null && dsEmployee.Tables.Count > 0)
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
    }
}