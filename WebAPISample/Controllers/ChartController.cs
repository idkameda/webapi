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
using System.Configuration;
using Newtonsoft.Json;

namespace WebAPISample.Controllers
{ 
    [Route("api/Chart")]
    public class ChartController : ApiController
    {
        // GET: Chart
        public IHttpActionResult Get(Int32 YearIn)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            DataSet dsResult = new DataSet("Summary");
            OutputObject obj = new OutputObject();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand objSqlCommand = new SqlCommand("usp_GET_Chart_Details", con);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                try
                {
                    objSqlCommand.Parameters.Add("@YearIN", SqlDbType.Int);
                    objSqlCommand.Parameters["@YearIN"].Value = YearIn.ToString();
                    objSqlDataAdapter.Fill(dsResult);
                    
                    if(dsResult!=null && dsResult.Tables.Count==3)
                    {
                        obj.Table = dsResult.Tables[0].AsEnumerable().Select(r => r.Field<string>("val")).ToArray();
                        obj.Table1 = dsResult.Tables[1].AsEnumerable().Select(r => r.Field<string>("val")).ToArray();
                        obj.Table2 = dsResult.Tables[2].AsEnumerable().Select(r => r.Field<string>("val")).ToArray();

                        //var json = JsonConvert.SerializeObject(obj);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return Ok(JsonConvert.SerializeObject(obj));
        }
    }
    public class OutputObject
    {
        public string[] Table { get; set; }
        public string[] Table1 { get; set; }
        public string[] Table2 { get; set; }
    }

}
