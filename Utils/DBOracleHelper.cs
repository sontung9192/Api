using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;

namespace Utils
{
    /// <summary>
    /// Summary description for DBHelper
    /// 2009-08-08 11:24:05
    /// </summary>
    public class DBOracleHelper
    {
        private string _cnnString = "";
        public string cnnString { get { return _cnnString; } set { _cnnString = value; } }

        /// <summary>
        /// Chuẩn hoá connection string
        /// </summary>
        public string FixCNN(string connStr, bool Pooling)
        {
            var aconnStr = connStr.Split(';');
            var sTemp = "";

            for (var i = 0; i < aconnStr.Length; i++)
            {
                if (aconnStr[i].ToLower().StartsWith("pooling=") ||
                    aconnStr[i].ToLower().StartsWith("min pool size=") ||
                    aconnStr[i].ToLower().StartsWith("max pool size=") ||
                    aconnStr[i].ToLower().StartsWith("connect timeout=")) continue;
                if (!aconnStr[i].Equals("")) sTemp += aconnStr[i] + ";";
            }

            if (Pooling)
            {
                sTemp += "Pooling=true;Min Pool Size=1;Max Pool Size=15;Connect Timeout=2;";
            }
            else
            {
                sTemp += "Pooling=false;Connect Timeout=45;";
            }
            return sTemp;
        }

        public DBOracleHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr"></param>
        public DBOracleHelper(string connStr)
        {
            _cnnString = connStr;
        }



        /// <summary>
        /// 
        /// </summary>
        private OracleConnection _ConnectionToDB;
        public OracleConnection ConnectionToDB
        {
            get { return _ConnectionToDB; }
            set { _ConnectionToDB = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (_cnnString == "")
            {
                throw new Exception("Connection String can not null");
            }
            _ConnectionToDB = OpenConnection();
        }


        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            CloseConnection(_ConnectionToDB);
        }




        /// <summary>
        /// return an Open OracleConnection
        /// </summary>

        public OracleConnection OpenConnection(string connectionString)
        {
            try
            {
                _cnnString = connectionString;
                return OpenConnection();
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
        }





        /// <summary>
        /// return an Open OracleConnection
        /// </summary>
        public OracleConnection OpenConnection()
        {
            if (_cnnString == "")
            {
                throw new Exception("Connection String can not null");
            }

            OracleConnection myOracleConnection;

            try
            {
                myOracleConnection = new OracleConnection(FixCNN(_cnnString, true));
                myOracleConnection.Open();
                return myOracleConnection;
            }
            catch (Exception)
            {
                // De phong truong hop bi max pool thi se fix lai connection string pooling=false
                myOracleConnection = new OracleConnection(FixCNN(_cnnString, false));
                myOracleConnection.Open();
                return myOracleConnection;
                // throw (new Exception(myException.Message));
            }
        }





        /// <summary>
        /// close an OracleConnection
        /// </summary>
        public void CloseConnection(OracleConnection myOracleConnection)
        {
            try
            {
                if (myOracleConnection != null)
                {
                    if (myOracleConnection.State == ConnectionState.Open)
                    {
                        myOracleConnection.Close();
                    }
                }
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
        }





        # region GetDataTable
        public DataTable GetDataTable(OracleCommand OracleCommand)
        {
            OracleConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    OracleCommand.Connection = conn;
                }
                else
                {
                    OracleCommand.Connection = _ConnectionToDB;
                }
                var myDataAdapter = new OracleDataAdapter(OracleCommand);
                var dt = new DataTable();
                myDataAdapter.Fill(dt);
                myDataAdapter.Dispose();
                return dt;
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public DataTable GetDataTable(OracleCommand OracleCommand, params OracleParameter[] Parameters)
        {
            OracleCommand.Parameters.AddRange(Parameters);
            return GetDataTable(OracleCommand);
        }

        public DataTable GetDataTable(string strSQL)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return GetDataTable(OracleCommand);
        }

        public DataTable GetDataTable(string strSQL, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return GetDataTable(OracleCommand, Parameters);
        }
        #endregion

        # region ExecuteScalar
        public object ExecuteScalar(OracleCommand OracleCommand)
        {
            OracleConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    OracleCommand.Connection = conn;
                }
                else
                {
                    OracleCommand.Connection = _ConnectionToDB;
                }
                return OracleCommand.ExecuteScalar();
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public object ExecuteScalar(OracleCommand OracleCommand, params OracleParameter[] Parameters)
        {
            OracleCommand.Parameters.AddRange(Parameters);
            return ExecuteScalar(OracleCommand);
        }

        public object ExecuteScalar(string strSQL)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return ExecuteScalar(OracleCommand);
        }

        public object ExecuteScalar(string strSQL, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return ExecuteScalar(OracleCommand, Parameters);
        }
        #endregion

        # region ExecuteNonQuery
        public int ExecuteNonQuery(OracleCommand OracleCommand)
        {
            OracleConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    OracleCommand.Connection = conn;
                }
                else
                {
                    OracleCommand.Connection = _ConnectionToDB;
                }
                return OracleCommand.ExecuteNonQuery();
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public int ExecuteNonQuery(OracleCommand OracleCommand, params OracleParameter[] Parameters)
        {
            OracleCommand.Parameters.AddRange(Parameters);
            return ExecuteNonQuery(OracleCommand);
        }

        public int ExecuteNonQuery(string strSQL)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return ExecuteNonQuery(OracleCommand);
        }

        public int ExecuteNonQuery(string strSQL, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return ExecuteNonQuery(OracleCommand, Parameters);
        }
        #endregion

        #region ExecuteScalarSP
        public object ExecuteScalarSP(string SPName)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return ExecuteScalar(OracleCommand);
        }

        public object ExecuteScalarSP(string SPName, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return ExecuteScalar(OracleCommand, Parameters);
        }
        #endregion

        #region ExecuteNonQuerySP
        public int ExecuteNonQuerySP(string SPName)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return ExecuteNonQuery(OracleCommand);
        }

        public int ExecuteNonQuerySP(string SPName, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return ExecuteNonQuery(OracleCommand, Parameters);
        }
        #endregion

        #region GetDataTableSP
        public DataTable GetDataTableSP(string SPName)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return GetDataTable(OracleCommand);
        }

        public DataTable GetDataTableSP(string SPName, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return GetDataTable(OracleCommand, Parameters);
        }
        #endregion

        #region[ListGenerate]
        public List<T> GetList<T>(OracleCommand OracleCommand)
        {
            try
            {
                OracleCommand.Connection = _ConnectionToDB ?? OpenConnection();
                var dr = OracleCommand.ExecuteReader();
                if (dr == null || dr.FieldCount == 0) return null;
                var fCount = dr.FieldCount;
                var m_Type = typeof(T);
                var l_Property = m_Type.GetProperties();
                object obj;
                var m_List = new List<T>();
                string pName;
                while (dr.Read())
                {
                    obj = Activator.CreateInstance(m_Type);
                    for (var i = 0; i < fCount; i++)
                    {
                        pName = dr.GetName(i);
                        if (l_Property.Where(a => a.Name == pName).Select(a => a.Name).Count() <= 0) continue;
                        if (dr[i] != DBNull.Value)
                        {
                            try
                            {
                                m_Type.GetProperty(pName).SetValue(obj, dr[i], null);
                            } catch(Exception ex){}
                        }
                        else
                        {
                            try
                            {
                                m_Type.GetProperty(pName).SetValue(obj, null, null);
                            }
                            catch (Exception ex) { }
                        }
                    }
                    m_List.Add((T)obj);
                }
                dr.Close();
                return m_List;
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {

                CloseConnection(OracleCommand.Connection);

            }
        }

        public List<T> GetList<T>(OracleCommand OracleCommand, params OracleParameter[] Parameters)
        {
            OracleCommand.Parameters.AddRange(Parameters);
            return GetList<T>(OracleCommand);
        }

        public List<T> GetList<T>(string strSQL)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return GetList<T>(OracleCommand);
        }

        public List<T> GetList<T>(string strSQL, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(strSQL);
            OracleCommand.Parameters.AddRange(Parameters);
            return GetList<T>(OracleCommand);
        }
        #endregion

        #region[ListGenerateSP]
        public List<T> GetListSP<T>(string SPName)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return GetList<T>(OracleCommand);
        }

        public List<T> GetListSP<T>(string SPName, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return GetList<T>(OracleCommand, Parameters);
        }
        #endregion

        #region[GetInstance]
        public T GetInstance<T>(OracleCommand OracleCommand)
        {
            try
            {
                T temp = default(T);

                OracleCommand.Connection = _ConnectionToDB ?? OpenConnection();
                var dr = OracleCommand.ExecuteReader();
                if (dr.Read())
                {
                    var fCount = dr.FieldCount;
                    var m_Type = typeof(T);
                    var l_Property = m_Type.GetProperties();
                    object obj;
                    var m_List = new List<T>();
                    string pName;

                    obj = Activator.CreateInstance(m_Type);
                    for (var i = 0; i < fCount; i++)
                    {
                        pName = dr.GetName(i);
                        if (l_Property.Where(a => a.Name == pName).Select(a => a.Name).Count() <= 0) continue;
                        if (dr[i] != DBNull.Value)
                        {
                            m_Type.GetProperty(pName).SetValue(obj, dr[i], null);
                        }
                        else
                        {
                            m_Type.GetProperty(pName).SetValue(obj, null, null);
                        }
                    }
                    dr.Close();
                    return (T)obj;
                }
                else
                {
                    return temp;
                }
            }
            catch (OracleException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                CloseConnection(OracleCommand.Connection);
            }
        }

        public T GetInstance<T>(OracleCommand OracleCommand, params OracleParameter[] Parameters)
        {
            OracleCommand.Parameters.AddRange(Parameters);
            return GetInstance<T>(OracleCommand);
        }

        public T GetInstance<T>(string strSQL)
        {
            var OracleCommand = new OracleCommand(strSQL);
            return GetInstance<T>(OracleCommand);
        }

        public T GetInstance<T>(string strSQL, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(strSQL);
            OracleCommand.Parameters.AddRange(Parameters);
            return GetInstance<T>(OracleCommand);
        }
        #endregion

        #region[GetInstanceSP]
        public T GetInstanceSP<T>(string SPName)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return GetInstance<T>(OracleCommand);
        }

        public T GetInstanceSP<T>(string SPName, params OracleParameter[] Parameters)
        {
            var OracleCommand = new OracleCommand(SPName) { CommandType = CommandType.StoredProcedure };
            return GetInstance<T>(OracleCommand, Parameters);
        }
        #endregion

    }
}