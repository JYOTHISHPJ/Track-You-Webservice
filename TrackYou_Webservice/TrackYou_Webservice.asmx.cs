using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Track.DAL;

namespace TrackYou_Webservice
{
    public class TrackYou_Webservice : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void REVZ_SelUserBasedTrkID(string TRACK_ID)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> dicParam = new Dictionary<string, object>();

                sbQuery.Append("select R_US_NAME  from R_STD_USER WHERE R_US_YN ='Y' AND R_US_ID =(SELECT R_R_US_ID  FROM R_TRK_COORDINATE WHERE R_TRACKING_ID   ='" + TRACK_ID + "') ");
                DataTable dt = DataAccess.ExecuteDataset(dicParam, sbQuery.ToString()).Tables[0];

                var JSONString = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    string R_US_NAME = dt.Rows[0]["R_US_NAME"].ToString();


                    JSONString.Append("{");
                    JSONString.Append("\"");
                    JSONString.Append("USER_NAME");
                    JSONString.Append("\"");
                    JSONString.Append(":");
                    JSONString.Append("\"");
                    JSONString.Append(R_US_NAME);
                    JSONString.Append("\"");



                    JSONString.Append("}");

                }


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JSONString);
            }
            catch
            {

            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void REVZ_SelectLATLON(string TRACKING_ID)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> dicParam = new Dictionary<string, object>();

                sbQuery.Append("select R_TRK_LAT,R_TRK_LON from R_TRK_COORDINATE WHERE R_TRK_YN ='Y' AND R_TRACKING_ID ='" + TRACKING_ID + "' ");
                DataTable dt = DataAccess.ExecuteDataset(dicParam, sbQuery.ToString()).Tables[0];

                var JSONString = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    string TRK_LAT = dt.Rows[0]["R_TRK_LAT"].ToString();
                    string TRK_LON = dt.Rows[0]["R_TRK_LON"].ToString();

                    JSONString.Append("{");
                    JSONString.Append("\"");
                    JSONString.Append("TRK_LAT");
                    JSONString.Append("\"");
                    JSONString.Append(":");
                    JSONString.Append("\"");
                    JSONString.Append(TRK_LAT);
                    JSONString.Append("\"");
                    JSONString.Append(",");

                    JSONString.Append("\"");
                    JSONString.Append("TRK_LON");
                    JSONString.Append("\"");
                    JSONString.Append(":");
                    JSONString.Append("\"");
                    JSONString.Append(TRK_LON);
                    JSONString.Append("\"");


                    JSONString.Append("}");

                }


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JSONString);
            }
            catch
            {

            }
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void REVZ_RegisterUser(string MOBILE, string NAME)
        {
            try
            {
                string RESULT = string.Empty;
                string USER_ID = string.Empty;

                //  pErrAction = string.Empty;
                // pStatus = string.Empty;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MOB_NO", SqlDbType.VarChar);
                parameters[0].SqlValue = MOBILE;
                parameters[0].Direction = ParameterDirection.Input;

                parameters[1] = new SqlParameter("@NAME", SqlDbType.VarChar);
                parameters[1].SqlValue = NAME;
                parameters[1].Direction = ParameterDirection.Input;

                parameters[2] = new SqlParameter("@RESULT", SqlDbType.Int);
                parameters[2].SqlValue = new string(new char[50]);
                parameters[2].Direction = ParameterDirection.Output;

                //parameters[2] = new SqlParameter("@RESULT", SqlDbType.Int);
                //parameters[2].SqlValue = new string(new char[50]);
                //parameters[2].Direction = ParameterDirection.Output;

                parameters[3] = new SqlParameter("@USR_ID", SqlDbType.Int);
                parameters[3].SqlValue = new string(new char[50]);
                parameters[3].Direction = ParameterDirection.Output;

                DataAccess.ExecuteProcedure(parameters, "DPRC_REGISTER_REVZ");


                RESULT = parameters[2].Value.ToString();
                USER_ID = parameters[3].Value.ToString();

                var JSONString = new StringBuilder();
                if (RESULT != null)
                {


                    JSONString.Append("{");
                    JSONString.Append("\"");
                    JSONString.Append("RESULT");
                    JSONString.Append("\"");
                    JSONString.Append(":");
                    JSONString.Append("\"");
                    JSONString.Append(RESULT);
                    JSONString.Append("\"");
                    JSONString.Append(",");

                    JSONString.Append("\"");
                    JSONString.Append("USER_ID");
                    JSONString.Append("\"");
                    JSONString.Append(":");
                    JSONString.Append("\"");
                    JSONString.Append(USER_ID);
                    JSONString.Append("\"");


                    JSONString.Append("}");

                }
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JSONString);
            }
            catch (Exception)
            {

            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void REVZ_SelectTrackingId(string USR_ID)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> dicParam = new Dictionary<string, object>();

                sbQuery.Append("select R_TRACKING_ID from R_TRK_COORDINATE WHERE R_R_US_ID='" + USR_ID + "' and R_TRK_YN ='Y'  ");
                DataTable dt = DataAccess.ExecuteDataset(dicParam, sbQuery.ToString()).Tables[0];

                var JSONString = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    string TRACKING_ID = dt.Rows[0]["R_TRACKING_ID"].ToString();


                    JSONString.Append("{");
                    JSONString.Append("\"");
                    JSONString.Append("TRACK_ID");
                    JSONString.Append("\"");
                    JSONString.Append(":");
                    JSONString.Append("\"");
                    JSONString.Append(TRACKING_ID);
                    JSONString.Append("\"");
                    JSONString.Append("}");

                }


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JSONString);
            }
            catch
            {

            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void REVZ_UpdateTrackingID(string TrackingId, string USR_ID)
        {

            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam["USR_ID"] = USR_ID;
                dicParam["TrackingId"] = TrackingId;

                sbQuery.Append("UPDATE R_TRK_COORDINATE set R_TRACKING_ID ='" + TrackingId + "' where R_R_US_ID='" + USR_ID + "' and R_TRK_YN ='Y' ");
                int RESULT = DataAccess.ExecuteFinalQuery(dicParam, sbQuery.ToString());
                var JSONString = new StringBuilder();
                JSONString.Append("{");
                JSONString.Append("\"");
                JSONString.Append("RESULT");
                JSONString.Append("\"");
                JSONString.Append(":");
                JSONString.Append("\"");
                JSONString.Append(RESULT);
                JSONString.Append("\"");
                JSONString.Append("}");


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JSONString);
            }
            catch
            {

            }
        }

        [WebMethod]
        public void REVZ_UpdateLatLon(string latitude, string longitude, string USR_ID)
        {

            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam["USR_ID"] = USR_ID;
                dicParam["latitude"] = latitude;
                dicParam["longitude"] = longitude;

                sbQuery.Append("UPDATE R_TRK_COORDINATE set R_TRK_LAT='" + latitude + "',R_TRK_LON ='" + longitude + "' where R_R_US_ID='" + USR_ID + "' and R_TRK_YN ='Y' ");
                DataAccess.ExecuteQueryScalar(dicParam, sbQuery.ToString());
            }
            catch
            {

            }
        }

     



    }


  
}
