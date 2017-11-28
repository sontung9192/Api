using Models.AddMailTrip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using Utils;

namespace ApiConnectOracle.Models
{
    public class E1I2DA
    {
        public DataSet GetListItem(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataSet ds = new DataSet();
            try
            {

                if (VNPEOracle.VnpeConnection.State != ConnectionState.Open)
                {
                    VNPEOracle.VnpeConnection.ConnectionString = VNPEOracle.ConnectionString;
                    VNPEOracle.VnpeConnection.Open();
                }
                OracleCommand mCommand = VNPEOracle.VnpeConnection.CreateCommand();

                mCommand.CommandText = "EMS_BCCP.GetListItem_Di";
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.CommandTimeout = 20000;
                mCommand.Parameters.Add("v_MaBcDong", OracleType.Int32).Value = v_MaBcDong;
                mCommand.Parameters.Add("v_MaBcNhan", OracleType.Int32).Value = v_MaBcNhan;
                mCommand.Parameters.Add("v_LoaiChuyenThu", OracleType.Int32).Value = v_LoaiChuyenThu;

                mCommand.Parameters.Add("v_LoaiDichVu", OracleType.VarChar, 3).Value = v_LoaiDichVu;
                mCommand.Parameters.Add("v_Ngay", OracleType.Int32).Value = v_Ngay;
                mCommand.Parameters.Add("v_SoChuyenThu", OracleType.Int32).Value = v_SoChuyenThu;

                mCommand.Parameters.Add("v_ListPostBag", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter mAdapter = new OracleDataAdapter();
                mAdapter = new OracleDataAdapter(mCommand);

                mAdapter.Fill(ds, "EMS_BCCP.GetListItem_Di");
            }
            catch (System.Exception e)
            {
                ds = null;
            }

            return ds;
        }
        public DataSet GetListMailTrip(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataSet ds = new DataSet();
            try
            {

                if (VNPEOracle.VnpeConnection.State != ConnectionState.Open)
                {
                    VNPEOracle.VnpeConnection.ConnectionString = VNPEOracle.ConnectionString;
                    VNPEOracle.VnpeConnection.Open();
                }
                OracleCommand mCommand = VNPEOracle.VnpeConnection.CreateCommand();

                mCommand.CommandText = "EMS_BCCP.GetListMailTrip_Di";
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.CommandTimeout = 20000;
                mCommand.Parameters.Add("v_MaBcDong", OracleType.Int32).Value = v_MaBcDong;
                mCommand.Parameters.Add("v_MaBcNhan", OracleType.Int32).Value = v_MaBcNhan;
                mCommand.Parameters.Add("v_LoaiChuyenThu", OracleType.Int32).Value = v_LoaiChuyenThu;

                mCommand.Parameters.Add("v_LoaiDichVu", OracleType.VarChar, 3).Value = v_LoaiDichVu;
                mCommand.Parameters.Add("v_Ngay", OracleType.Int32).Value = v_Ngay;
                mCommand.Parameters.Add("v_SoChuyenThu", OracleType.Int32).Value = v_SoChuyenThu;

                mCommand.Parameters.Add("v_ListMailTrip", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter mAdapter = new OracleDataAdapter();
                mAdapter = new OracleDataAdapter(mCommand);

                mAdapter.Fill(ds, "EMS_BCCP.GetListMailTrip_Di");
            }
            catch (System.Exception e)
            {
                ds = null;
            }

            return ds;
        }
        public DataSet GetListPostBag(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataSet ds = new DataSet();
            try
            {

                if (VNPEOracle.VnpeConnection.State != ConnectionState.Open)
                {
                    VNPEOracle.VnpeConnection.ConnectionString = VNPEOracle.ConnectionString;
                    VNPEOracle.VnpeConnection.Open();
                }
                OracleCommand mCommand = VNPEOracle.VnpeConnection.CreateCommand();

                mCommand.CommandText = "EMS_BCCP.GetListPostBag_Di";
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.CommandTimeout = 20000;
                mCommand.Parameters.Add("v_MaBcDong", OracleType.Int32).Value = v_MaBcDong;
                mCommand.Parameters.Add("v_MaBcNhan", OracleType.Int32).Value = v_MaBcNhan;
                mCommand.Parameters.Add("v_LoaiChuyenThu", OracleType.Int32).Value = v_LoaiChuyenThu;

                mCommand.Parameters.Add("v_LoaiDichVu", OracleType.VarChar, 3).Value = v_LoaiDichVu;
                mCommand.Parameters.Add("v_Ngay", OracleType.Int32).Value = v_Ngay;
                mCommand.Parameters.Add("v_SoChuyenThu", OracleType.Int32).Value = v_SoChuyenThu;

                mCommand.Parameters.Add("v_ListPostBag", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter mAdapter = new OracleDataAdapter();
                mAdapter = new OracleDataAdapter(mCommand);

                mAdapter.Fill(ds, "EMS_BCCP.GetListPostBag_Di");
            }
            catch (System.Exception e)
            {
                ds = null;
            }

            return ds;
        }
        public DataSet GetListDispatch(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataSet ds = new DataSet();
            try
            {

                if (VNPEOracle.VnpeConnection.State != ConnectionState.Open)
                {
                    VNPEOracle.VnpeConnection.ConnectionString = VNPEOracle.ConnectionString;
                    VNPEOracle.VnpeConnection.Open();
                }
                OracleCommand mCommand = VNPEOracle.VnpeConnection.CreateCommand();

                mCommand.CommandText = "EMS_BCCP.GetListDispatch_Di";
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.CommandTimeout = 20000;
                mCommand.Parameters.Add("v_MaBcDong", OracleType.Int32).Value = v_MaBcDong;
                mCommand.Parameters.Add("v_MaBcNhan", OracleType.Int32).Value = v_MaBcNhan;
                mCommand.Parameters.Add("v_LoaiChuyenThu", OracleType.Int32).Value = v_LoaiChuyenThu;

                mCommand.Parameters.Add("v_LoaiDichVu", OracleType.VarChar, 3).Value = v_LoaiDichVu;
                mCommand.Parameters.Add("v_Ngay", OracleType.Int32).Value = v_Ngay;
                mCommand.Parameters.Add("v_SoChuyenThu", OracleType.Int32).Value = v_SoChuyenThu;

                mCommand.Parameters.Add("v_ListPostBag", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter mAdapter = new OracleDataAdapter();
                mAdapter = new OracleDataAdapter(mCommand);

                mAdapter.Fill(ds, "EMS_BCCP.GetListDispatch_Di");
            }
            catch (System.Exception e)
            {
                ds = null;
            }

            return ds;
        }
        //public DataSet GetListItem_Den(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        if (VNPEOracle.VnpeConnection.State != ConnectionState.Open)
        //        {
        //            VNPEOracle.VnpeConnection.ConnectionString = VNPEOracle.ConnectionString;
        //            VNPEOracle.VnpeConnection.Open();
        //        }
        //        OracleCommand mCommand = VNPEOracle.VnpeConnection.CreateCommand();

        //        mCommand.CommandText = "EMS_BCCP.GetListItem_Den";
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.CommandTimeout = 20000;
        //        mCommand.Parameters.Add("v_MaBcDong", OracleType.Int32).Value = v_MaBcDong;
        //        mCommand.Parameters.Add("v_MaBcNhan", OracleType.Int32).Value = v_MaBcNhan;
        //        mCommand.Parameters.Add("v_LoaiChuyenThu", OracleType.Int32).Value = v_LoaiChuyenThu;

        //        mCommand.Parameters.Add("v_LoaiDichVu", OracleType.VarChar, 3).Value = v_LoaiDichVu;
        //        mCommand.Parameters.Add("v_Ngay", OracleType.Int32).Value = v_Ngay;
        //        mCommand.Parameters.Add("v_SoChuyenThu", OracleType.Int32).Value = v_SoChuyenThu;

        //        mCommand.Parameters.Add("v_ListPostBag", OracleType.Cursor).Direction = ParameterDirection.Output;
        //        OracleDataAdapter mAdapter = new OracleDataAdapter();
        //        mAdapter = new OracleDataAdapter(mCommand);

        //        mAdapter.Fill(ds, "EMS_BCCP.GetListItem_Den");
        //    }
        //    catch (System.Exception e)
        //    {
        //        ds = null;
        //    }

        //    return ds;
        //}

        public static DataTable GetListChThu(int bccp, int ngay)
        {
            var conn = Config.getConnectionOracle();
            try
            {
                string sql = "select  MABC,CHTHU,NGAY,count(*) AS SL from E2_BD13_DI t Where BCCP = :vBCCP and  NGAY =  :vNgay Group by MABC,CHTHU,BCCP,NGAY";
                // string sql = "select  MABC,CHTHU,NGAY,count(*) AS SL from E2_BD13_DI Group by MABC,CHTHU,BCCP,NGAY";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("vBCCP", OracleType.Int32).Value = bccp;
                cmd.Parameters.Add("vNgay", OracleType.Int32).Value = ngay;
                //cmd.Parameters.AddWithValue("@_vBCCP", bccp);
                //cmd.Parameters.AddWithValue("@_vNgay", ngay);
                OracleDataAdapter sqlDa = new OracleDataAdapter(cmd);
                DataTable data = new DataTable();
                sqlDa.Fill(data);
                return data;
            }
            catch (OracleException ex)
            {
                string err = string.Format("[ERR] ex={0}", ex.Message);
                LogHelper.LogInfo(err, "AddMailTrip");
                return new DataTable();
            }
            finally
            {
                // Close Connection
                conn.Close();
                conn.Dispose();
            }
        }
        public static List<E2_BD13_DIDTO> GetListChThu2(int bccp, int ngay)
        {
            try
            {
                var conn = Config.getConnectionOracle();
                conn.Open();
                // Open Connection
                string sql = "select  MABC,CHTHU,NGAY,count(*) AS SL from E2_BD13_DI t Where BCCP = :vBCCP and  NGAY =  :vNgay Group by MABC,CHTHU,BCCP,NGAY";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("vBCCP", OracleType.Int32).Value = bccp;
                cmd.Parameters.Add("vNgay", OracleType.Int32).Value = ngay;
                IDataReader reader = (IDataReader)cmd.ExecuteReader();
                List<E2_BD13_DIDTO> result = new List<E2_BD13_DIDTO>();
                if (reader != null)
                {
                    result = DbExtensions.ToList<E2_BD13_DIDTO>(reader);
                }
                conn.Close();
                conn.Dispose();
                return result;
            }
            catch(Exception ex)
            {
                string err = string.Format("[ERR] ex={0}", ex.Message);
                LogHelper.LogInfo(err, "AddMailTrip");
                return new List<E2_BD13_DIDTO>();
            }
        }
        public static List<ListMailTripDTO> GetListMailTrip_1(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {

                // Open Connection
                //  string sql = "select  MABC,CHTHU,NGAY,count(*) AS SL from E2_BD13_DI t Where BCCP = :vBCCP and  NGAY =  :vNgay Group by MABC,CHTHU,BCCP,NGAY";
                // OracleCommand cmd = new OracleCommand(sql, conn);
                if (VNPEOracle.VnpeConnection.State != ConnectionState.Open)
                {
                    VNPEOracle.VnpeConnection.ConnectionString = VNPEOracle.ConnectionString;
                    VNPEOracle.VnpeConnection.Open();
                }
                OracleCommand cmd = VNPEOracle.VnpeConnection.CreateCommand();

                cmd.CommandText = "EMS_BCCP.GetListMailTrip_Di";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 20000;
                cmd.Parameters.Add("v_MaBcDong", OracleType.Int32).Value = v_MaBcDong;
                cmd.Parameters.Add("v_MaBcNhan", OracleType.Int32).Value = v_MaBcNhan;
                cmd.Parameters.Add("v_LoaiChuyenThu", OracleType.Int32).Value = v_LoaiChuyenThu;

                cmd.Parameters.Add("v_LoaiDichVu", OracleType.VarChar, 3).Value = v_LoaiDichVu;
                cmd.Parameters.Add("v_Ngay", OracleType.Int32).Value = v_Ngay;
                cmd.Parameters.Add("v_SoChuyenThu", OracleType.Int32).Value = v_SoChuyenThu;

                cmd.Parameters.Add("v_ListMailTrip", OracleType.Cursor).Direction = ParameterDirection.Output;

                IDataReader reader = (IDataReader)cmd.ExecuteReader();
                List<ListMailTripDTO> result = new List<ListMailTripDTO>();
                if (reader != null)
                {
                    result = DbExtensions.ToList<ListMailTripDTO>(reader);
                }
                VNPEOracle.VnpeConnection.Close();
                VNPEOracle.VnpeConnection.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR] ex={0}", ex.Message);
                LogHelper.LogInfo(err, "AddMailTrip");
                return new List<ListMailTripDTO>();
            }
        }
    }
}