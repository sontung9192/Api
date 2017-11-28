using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace ApiConnectOracle.Models
{
    public class VNPEOracle
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Ems.Bccp.Communication.Properties.Settings.EmsBccpConnectionString"].ConnectionString;
        public static OracleConnection VnpeConnection = new OracleConnection(ConnectionString);
    }
}