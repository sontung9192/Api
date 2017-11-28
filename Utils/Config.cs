using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Config
    {
        public static string MakeRefId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(999).ToString().PadLeft(3, '0');
        }
        public static SqlConnection getConnectionKhieuNai()
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString.KhieuNai"].ConnectionString);
            return sqlConn;
        }
        public static SqlConnection getConnectionSO_LIEU_BPBK()
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString.SOLIEUBPBK"].ConnectionString);
            return sqlConn;
        }
        public static string getConnection()
        {
            var sqlConn = ConfigurationManager.ConnectionStrings["ConnectionString.Data"].ConnectionString;
            return sqlConn;
        }
        public static string getConnectioneEnterPrise()
        {
            var sqlConn = ConfigurationManager.ConnectionStrings["ConnectionString.EnterPrise"].ConnectionString;
            return sqlConn;
        }
        public static string getConnectioneEnterPrisetest()
        {
            var sqlConn = ConfigurationManager.ConnectionStrings["ConnectionString.EnterPriseTest"].ConnectionString;
            return sqlConn;
        }
        public static OracleConnection getConnectionOracle()
        {
            var oracleConn = new OracleConnection(ConfigurationManager.ConnectionStrings["Ems.Bccp.Communication.Properties.Settings.EmsBccpConnectionString"].ConnectionString);
            return oracleConn;
        }
    }
}
