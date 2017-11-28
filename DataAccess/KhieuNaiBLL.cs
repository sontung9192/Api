using Models.KhieuNai;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataAccess
{
    public class KhieuNaiBLL
    {
        static string strQuery;
        public static DataTable GetListPostSoLieuBySohieu(string sohieu)
        {
            var conn = Config.getConnectionSO_LIEU_BPBK();

            //Create Command object
            SqlCommand nonqueryCommand = conn.CreateCommand();

            try
            {
                // Open Connection
                conn.Open();
                string sql = "SELECT  BC_GOI, BC_NHAN,NGAY_GOI,CHUYEN_THU,TUI_THU,HTVCHUYEN,B_DICH_VU FROM  POST_SO_LIEU WHERE (SO_HIEU=@_SOHIEU)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //List<SqlParameter> parameters = new List<SqlParameter>
                //    {
                //        new SqlParameter("@_SOHIEU", SqlDbType.NVarChar) { Value = sohieu },

                //    };
                //cmd.Parameters.Add(parameters);
                cmd.Parameters.AddWithValue("@_SOHIEU", sohieu);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                sqlDa.Fill(data);
                return data;
            }

            catch (SqlException ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [POST_SO_LIEU] -  SO_HIEU={0},ex={1}", sohieu, ex.Message);
                LogHelper.LogInfo(err, "KhieuNai");
                return new DataTable();
            }
            finally
            {
                // Close Connection
                conn.Close();
                conn.Dispose();
            }
        }
        public static DataTable GetListSYSCONBySTTTinh(string sttTinh)
        {
            var conn = Config.getConnectionKhieuNai();
            //Create Command object
            SqlCommand nonqueryCommand = conn.CreateCommand();
            try
            {
                // Open Connection
                conn.Open();
                string sql = "SELECT * FROM SYS_CON WHERE (STTTINH=@_STTTINH)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@_STTTINH", sttTinh);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                sqlDa.Fill(data);
                return data;
            }
            catch (SqlException ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [SYS_CON] -  STTTINH={0},ex={1}", sttTinh, ex.Message);
                LogHelper.LogInfo(err, "KhieuNai");
                return new DataTable();
            }
            finally
            {
                // Close Connection
                conn.Close();
                conn.Dispose();
            }
        }
        public static DataTable GetThoiGianByLoaiDV(int? sId)
        {
            var conn = Config.getConnectionKhieuNai();
            //Create Command object
            SqlCommand nonqueryCommand = conn.CreateCommand();
            try
            {
                // Open Connection
                conn.Open();
                string sql = "SELECT THOIGIAN FROM DM_DICHVU WHERE (ID=@_ID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@_ID", sId);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                sqlDa.Fill(data);
                return data;
            }
            catch (SqlException ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [DM_DICHVU] -  ID={0},ex={1}", sId, ex.Message);
                LogHelper.LogInfo(err, "KhieuNai");
                return new DataTable();
            }
            finally
            {
                // Close Connection
                conn.Close();
                conn.Dispose();
            }
        }
        public static DataTable CheckNgayLamViec(string firstday, string lastday)
        {
            var conn = Config.getConnectionKhieuNai();
            //Create Command object
            SqlCommand nonqueryCommand = conn.CreateCommand();
            try
            {
                // Open Connection
                conn.Open();
                string sql = "SELECT COUNT(NGAY) AS NGAYLE FROM SYS_NGAY  WHERE CONVERT(smalldatetime,NGAY,101)>='" + firstday + " 00:00:01 AM'  AND CONVERT(smalldatetime,NGAY,101)<='" + lastday + " 23:59:59 PM'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                sqlDa.Fill(data);
                return data;
            }
            catch (SqlException ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [SYS_NGAY] -  firstday={0},lastday={1},ex={2}", firstday, lastday, ex.Message);
                LogHelper.LogInfo(err, "KhieuNai");
                return new DataTable();
            }
            finally
            {
                // Close Connection
                conn.Close();
                conn.Dispose();
            }
        }

        public static int SYSCONUpdateName(string name, string tinh)
        {
            string strQuery;
            SqlCommand cmd;
            strQuery = "UPDATE SYS_CON SET STT=1,NAM=@_Name WHERE STTTINH=@_STTTINH";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@_Name", name);
            cmd.Parameters.AddWithValue("@_STTTINH", tinh);
            var result = InsertUpdateData(cmd, Config.getConnectionKhieuNai());
            return result;
        }
        public static int SYSCONUpdateSTT(string tinh)
        {

            SqlCommand cmd;
            strQuery = "UPDATE SYS_CON SET STT=STT+1 WHERE STTTINH=@_STTTINH";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@_STTTINH", tinh);
            var result = InsertUpdateData(cmd, Config.getConnectionKhieuNai());
            return result;
        }
        public static int DT_KhieuNai_DaXem_Update(string ma, string idkhieunai, string daxem)
        {
            SqlCommand cmd;
            strQuery = "UPDATE DT_KHIEUNAI_DAXEM SET DAXEM=@_DAXEM WHERE IDKHIEUNAI=@_IDKHIEUNAI AND USERNAME=@_USERNAME";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@_IDKHIEUNAI", idkhieunai);
            cmd.Parameters.AddWithValue("@_USERNAME", ma);
            cmd.Parameters.AddWithValue("@_DAXEM", daxem);
            var result = InsertUpdateData(cmd, Config.getConnectionKhieuNai());
            return result;
        }
        public static int DT_KhieuNai_DaXem_UpdateAllNotEqual(string ma, string idkhieunai, string daxem)
        {
            SqlCommand cmd;
            strQuery = "UPDATE DT_KHIEUNAI_DAXEM SET DAXEM=@_DAXEM WHERE IDKHIEUNAI=@_IDKHIEUNAI AND USERNAME <> @_USERNAME";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@_IDKHIEUNAI", idkhieunai);
            cmd.Parameters.AddWithValue("@_USERNAME", ma);
            cmd.Parameters.AddWithValue("@_DAXEM", daxem);
            var result = InsertUpdateData(cmd, Config.getConnectionKhieuNai());
            return result;
        }
        public static int DT_KhieuNai_DaXem_InsertByMaNguoiThuLyTinh(string manguoithulytinh, string idkhieunai)
        {
            SqlCommand cmd;
            strQuery = "INSERT INTO DT_KHIEUNAI_DAXEM (IDKHIEUNAI,USERNAME,DATRUYEN) VALUES (@_IDKHIEUNAI,@_USERNAME,0)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@_IDKHIEUNAI", idkhieunai);
            cmd.Parameters.AddWithValue("@_USERNAME", manguoithulytinh);
            var result = InsertUpdateData(cmd, Config.getConnectionKhieuNai());
            return result;
        }
        public static int DT_KhieuNai_DaXem_InsertByNguoiNhap(string nguoinhap, string idkhieunai)
        {
            SqlCommand cmd;
            strQuery = "INSERT INTO DT_KHIEUNAI_DAXEM (IDKHIEUNAI,USERNAME,DAXEM,DATRUYEN) VALUES (@_IDKHIEUNAI,@_USERNAME,1,0)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@_IDKHIEUNAI", idkhieunai);
            cmd.Parameters.AddWithValue("@_USERNAME", nguoinhap);
            var result = InsertUpdateData(cmd, Config.getConnectionKhieuNai());
            return result;
        }
        private DataTable GetData(SqlCommand cmd, SqlConnection conn)
        {
            DataTable dt = new DataTable();
            //var conn = Config.getConnectionKhieuNai();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                sda.Dispose();
                conn.Dispose();
            }
        }
        private static int InsertUpdateData(SqlCommand cmd, SqlConnection conn)
        {
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                int id = cmd.ExecuteNonQuery();
                return id;
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] ex={0}", ex.Message);
                LogHelper.LogInfo(err, "KhieuNai");
                return -1;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public static List<KhieuNaiDTO> GetListKhieuNai(ThoiGianDTO model)
        {
            var conn = Config.getConnectionKhieuNai();
            // Open Connection
            conn.Open();
            string sql = "";
            if (string.IsNullOrEmpty(model.NguoiNhap))
            {
                sql = "SELECT * FROM DT_KHIEUNAI WHERE (NGAYLAP >= @_FromDate AND NGAYLAP <= @_ToDate AND CHK=@_CHK)";
            }
            else
            {
                if (string.IsNullOrEmpty(model.CHK))
                {
                    sql = "SELECT * FROM DT_KHIEUNAI WHERE (NGUOINHAP=@_NGUOINHAP AND NGAYLAP >= @_FromDate AND NGAYLAP <= @_ToDate)";
                }
                else
                {
                    sql = "SELECT * FROM DT_KHIEUNAI WHERE (NGUOINHAP=@_NGUOINHAP AND NGAYLAP >= @_FromDate AND NGAYLAP <= @_ToDate AND CHK= @_CHK)";
                }
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrEmpty(model.NguoiNhap))
            {
                cmd.Parameters.AddWithValue("@_NGUOINHAP", model.NguoiNhap);
            }
            if (!string.IsNullOrEmpty(model.CHK))
            {
                cmd.Parameters.AddWithValue("@_CHK", model.CHK);
            }
            cmd.Parameters.AddWithValue("@_FromDate", model.FromDate);
            cmd.Parameters.AddWithValue("@_ToDate", model.ToDate);
            IDataReader reader = (IDataReader)cmd.ExecuteReader();
            List<KhieuNaiDTO> result = new List<KhieuNaiDTO>();
            if (reader != null)
            {
                result = DbExtensions.ToList<KhieuNaiDTO>(reader);
            }
            // Close Connection
            conn.Close();
            conn.Dispose();
            return result;
        }
        public static List<KhieuNaiTienTrinhDTO> GetListKhieuNaiTienTrinh(GetKhieuNaiTienTrinhDTO model)
        {
            var conn = Config.getConnectionKhieuNai();
            conn.Open();
            string sql = "";
            if (model.IDKhieuNai != "")
            {
                sql = "SELECT  * FROM DT_KHIEUNAI_TIENTRINH WHERE (IDKHIEUNAI=@_IDKHIEUNAI)";
            }
            else
            {
                sql = "SELECT * FROM DT_KHIEUNAI_TIENTRINH WHERE (NGAYNHAP >= @_FromDate AND NGAYNHAP <= @_ToDate AND (NGUOINHAP = @_USERNAME OR NGUOIXLYTIEP = @_USERNAME))";
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (model.IDKhieuNai != "")
            {
                cmd.Parameters.AddWithValue("@_IDKHIEUNAI", model.IDKhieuNai);
            }
            else
            {
                cmd.Parameters.AddWithValue("@_USERNAME", model.UserName);
                cmd.Parameters.AddWithValue("@_FromDate", model.FromDate);
                cmd.Parameters.AddWithValue("@_ToDate", model.ToDate);
            }
            IDataReader reader = (IDataReader)cmd.ExecuteReader();
            List<KhieuNaiTienTrinhDTO> result = new List<KhieuNaiTienTrinhDTO>();
            if (reader != null)
            {
                result = DbExtensions.ToList<KhieuNaiTienTrinhDTO>(reader);
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public static List<KhieuNaiUserDTO> GetListKhieuNaiUser(GetKhieuNaiUserDTO model)
        {
            var conn = Config.getConnectionKhieuNai();
            // Open Connection
            string sql = "";
            conn.Open();
            if (!string.IsNullOrEmpty(model.IdKhieuNai) && !string.IsNullOrEmpty(model.UserName))
            {
                sql = "SELECT * FROM DT_KHIEUNAI_USER WHERE IDKHIEUNAI = @_IDKHIEUNAI AND  USERNAME = @_UserName";
            }
            if (string.IsNullOrEmpty(model.IdKhieuNai) && !string.IsNullOrEmpty(model.UserName))
            {
                sql = "SELECT * FROM DT_KHIEUNAI_USER WHERE  USERNAME = @_UserName ";
            }
            if(!string.IsNullOrEmpty(model.IdKhieuNai) && string.IsNullOrEmpty(model.UserName))
            {
                sql = "SELECT * FROM DT_KHIEUNAI_USER WHERE IDKHIEUNAI = @_IDKHIEUNAI";
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrEmpty(model.IdKhieuNai))
            {
                cmd.Parameters.AddWithValue("@_IDKHIEUNAI", model.IdKhieuNai);
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                cmd.Parameters.AddWithValue("@_UserName", model.UserName);
            }
            IDataReader reader = (IDataReader)cmd.ExecuteReader();
            List<KhieuNaiUserDTO> result = new List<KhieuNaiUserDTO>();
            if (reader != null)
            {
                result = DbExtensions.ToList<KhieuNaiUserDTO>(reader);
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public static List<NguoiThuLyTinhDTO> GetListNguoiThuLyTinh(string matinh,string idkhieunai)
        {
            var conn = Config.getConnectionKhieuNai();
            // Open Connection
            string sql = "SELECT USERNAME, TENNV FROM  SYS_USER WHERE (STTTINH=" + matinh + " AND DAUMOI='1')  ";
            sql += "union  select distinct S.USERNAME, S.TENNV from SYS_USER S  with(nolock) ";
            sql += " inner join DT_KHIEUNAI_USER D on ( S.USERNAME = D.USERNAME and S.STTTINH =" + matinh + ")  ";
            sql += " where IDKHIEUNAI = '" + idkhieunai + "'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
           
            IDataReader reader = (IDataReader)cmd.ExecuteReader();
            List<NguoiThuLyTinhDTO> result = new List<NguoiThuLyTinhDTO>();
            if (reader != null)
            {
                result = DbExtensions.ToList<NguoiThuLyTinhDTO>(reader);
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
    }
}
