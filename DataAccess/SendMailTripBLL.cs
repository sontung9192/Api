using Models.AddMailTrip;
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
    public class SendMailTripBLL : DbHelper
    {
        readonly DbHelper _db = new DbHelper(Config.getConnectioneEnterPrise());
        public List<ListPostBagDTO> GetListPostBag_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_MaBcDong", SqlDbType.Int) { Value = v_MaBcDong },
                        new SqlParameter("@_MaBcNhan", SqlDbType.Int) { Value = v_MaBcNhan },
                        new SqlParameter("@_LoaiChuyenThu", SqlDbType.Int) { Value = v_LoaiChuyenThu },
                        new SqlParameter("@_LoaiDichVu", SqlDbType.VarChar) { Value = v_LoaiDichVu },
                        new SqlParameter("@_Ngay", SqlDbType.Int) { Value = v_Ngay },
                        new SqlParameter("@_SoChuyenThu", SqlDbType.Int) { Value = v_SoChuyenThu }

                    };
                var data = _db.GetListSP<ListPostBagDTO>("EMS_BCCP_GetListPostBag_Di", parameters.ToArray());
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListPostBag_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public List<ListMailTripV2DTO> GetListMailTrip_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_MaBcDong", SqlDbType.Int) { Value = v_MaBcDong },
                        new SqlParameter("@_MaBcNhan", SqlDbType.Int) { Value = v_MaBcNhan },
                        new SqlParameter("@_LoaiChuyenThu", SqlDbType.Int) { Value = v_LoaiChuyenThu },
                        new SqlParameter("@_LoaiDichVu", SqlDbType.VarChar) { Value = v_LoaiDichVu },
                        new SqlParameter("@_Ngay", SqlDbType.Int) { Value = v_Ngay },
                        new SqlParameter("@_SoChuyenThu", SqlDbType.Int) { Value = v_SoChuyenThu }

                    };
                var data = _db.GetListSP<ListMailTripV2DTO>("EMS_BCCP_GetListMailTrip_Di", parameters.ToArray());
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListMailTrip_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public List<ListDispatchDTO> GetListDispatch_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_MaBcDong", SqlDbType.Int) { Value = v_MaBcDong },
                        new SqlParameter("@_MaBcNhan", SqlDbType.Int) { Value = v_MaBcNhan },
                        new SqlParameter("@_LoaiChuyenThu", SqlDbType.Int) { Value = v_LoaiChuyenThu },
                        new SqlParameter("@_LoaiDichVu", SqlDbType.VarChar) { Value = v_LoaiDichVu },
                        new SqlParameter("@_Ngay", SqlDbType.Int) { Value = v_Ngay },
                        new SqlParameter("@_SoChuyenThu", SqlDbType.Int) { Value = v_SoChuyenThu }

                    };
                var data = _db.GetListSP<ListDispatchDTO>("EMS_BCCP_GetListDispatch_Di", parameters.ToArray());
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListDispatch_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public List<ListBatchDTO> GetListBatch_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_MaBcDong", SqlDbType.Int) { Value = v_MaBcDong },
                        new SqlParameter("@_MaBcNhan", SqlDbType.Int) { Value = v_MaBcNhan },
                        new SqlParameter("@_LoaiChuyenThu", SqlDbType.Int) { Value = v_LoaiChuyenThu },
                        new SqlParameter("@_LoaiDichVu", SqlDbType.VarChar) { Value = v_LoaiDichVu },
                        new SqlParameter("@_Ngay", SqlDbType.Int) { Value = v_Ngay },
                        new SqlParameter("@_SoChuyenThu", SqlDbType.Int) { Value = v_SoChuyenThu }

                    };
                var data = _db.GetListSP<ListBatchDTO>("EMS_BCCP_GetListBatch_Di", parameters.ToArray());
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListBatch_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public List<ListItemDTO> GetListItem_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_MaBcDong", SqlDbType.Int) { Value = v_MaBcDong },
                        new SqlParameter("@_MaBcNhan", SqlDbType.Int) { Value = v_MaBcNhan },
                        new SqlParameter("@_LoaiChuyenThu", SqlDbType.Int) { Value = v_LoaiChuyenThu },
                        new SqlParameter("@_LoaiDichVu", SqlDbType.VarChar) { Value = v_LoaiDichVu },
                        new SqlParameter("@_Ngay", SqlDbType.Int) { Value = v_Ngay },
                        new SqlParameter("@_SoChuyenThu", SqlDbType.Int) { Value = v_SoChuyenThu }

                    };
                var data = _db.GetListSP<ListItemDTO>("EMS_BCCP_GetListItem_Di", parameters.ToArray());
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][GetListItem_Di],ex={0}", ex.Message));
                return null;
            }
        }


        public DataSet DataSet_GetListBatch_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                DataSet ds = new DataSet("EMS_BCCP_GetListBatch_Di");
                using (SqlConnection conn = new SqlConnection(Config.getConnectioneEnterPrise()))
                {
                    SqlCommand sqlComm = new SqlCommand("EMS_BCCP_GetListBatch_Di", conn);
                    sqlComm.Parameters.AddWithValue("@_MaBcDong", v_MaBcDong);
                    sqlComm.Parameters.AddWithValue("@_MaBcNhan", v_MaBcNhan);
                    sqlComm.Parameters.AddWithValue("@_LoaiChuyenThu", v_SoChuyenThu);
                    sqlComm.Parameters.AddWithValue("@_LoaiDichVu", v_LoaiDichVu);
                    sqlComm.Parameters.AddWithValue("@_Ngay", v_Ngay);
                    sqlComm.Parameters.AddWithValue("@_SoChuyenThu", v_SoChuyenThu);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListBatch_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public DataSet DataSet_GetListItem_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                DataSet ds = new DataSet("EMS_BCCP_GetListItem_Di");
                using (SqlConnection conn = new SqlConnection(Config.getConnectioneEnterPrise()))
                {
                    SqlCommand sqlComm = new SqlCommand("EMS_BCCP_GetListItem_Di", conn);
                    sqlComm.Parameters.AddWithValue("@_MaBcDong", v_MaBcDong);
                    sqlComm.Parameters.AddWithValue("@_MaBcNhan", v_MaBcNhan);
                    sqlComm.Parameters.AddWithValue("@_LoaiChuyenThu", v_SoChuyenThu);
                    sqlComm.Parameters.AddWithValue("@_LoaiDichVu", v_LoaiDichVu);
                    sqlComm.Parameters.AddWithValue("@_Ngay", v_Ngay);
                    sqlComm.Parameters.AddWithValue("@_SoChuyenThu", v_SoChuyenThu);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListItem_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public DataSet DataSet_GetListDispatch_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {

            try
            {
                DataSet ds = new DataSet("GetListDispatch_Di");
                using (SqlConnection conn = new SqlConnection(Config.getConnectioneEnterPrise()))
                {
                    SqlCommand sqlComm = new SqlCommand("EMS_BCCP_GetListDispatch_Di", conn);
                    sqlComm.Parameters.AddWithValue("@_MaBcDong", v_MaBcDong);
                    sqlComm.Parameters.AddWithValue("@_MaBcNhan", v_MaBcNhan);
                    sqlComm.Parameters.AddWithValue("@_LoaiChuyenThu", v_SoChuyenThu);
                    sqlComm.Parameters.AddWithValue("@_LoaiDichVu", v_LoaiDichVu);
                    sqlComm.Parameters.AddWithValue("@_Ngay", v_Ngay);
                    sqlComm.Parameters.AddWithValue("@_SoChuyenThu", v_SoChuyenThu);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListItem_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public DataSet DataSet_GetListMailTrip_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                DataSet ds = new DataSet("GetListMailTrip_Di");
                using (SqlConnection conn = new SqlConnection(Config.getConnectioneEnterPrise()))
                {
                    SqlCommand sqlComm = new SqlCommand("EMS_BCCP_GetListMailTrip_Di", conn);
                    sqlComm.Parameters.AddWithValue("@_MaBcDong", v_MaBcDong);
                    sqlComm.Parameters.AddWithValue("@_MaBcNhan", v_MaBcNhan);
                    sqlComm.Parameters.AddWithValue("@_LoaiChuyenThu", v_SoChuyenThu);
                    sqlComm.Parameters.AddWithValue("@_LoaiDichVu", v_LoaiDichVu);
                    sqlComm.Parameters.AddWithValue("@_Ngay", v_Ngay);
                    sqlComm.Parameters.AddWithValue("@_SoChuyenThu", v_SoChuyenThu);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListMailTrip_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public DataSet DataSet_GetListPostBag_Di(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            try
            {
                DataSet ds = new DataSet("GetListPostBag_Di");
                using (SqlConnection conn = new SqlConnection(Config.getConnectioneEnterPrise()))
                {
                    SqlCommand sqlComm = new SqlCommand("EMS_BCCP_GetListPostBag_Di", conn);
                    sqlComm.Parameters.AddWithValue("@_MaBcDong", v_MaBcDong);
                    sqlComm.Parameters.AddWithValue("@_MaBcNhan", v_MaBcNhan);
                    sqlComm.Parameters.AddWithValue("@_LoaiChuyenThu", v_SoChuyenThu);
                    sqlComm.Parameters.AddWithValue("@_LoaiDichVu", v_LoaiDichVu);
                    sqlComm.Parameters.AddWithValue("@_Ngay", v_Ngay);
                    sqlComm.Parameters.AddWithValue("@_SoChuyenThu", v_SoChuyenThu);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetListPostBag_Di],ex={0}", ex.Message));
                return null;
            }
        }

        // bảng tạm check trạng thái
        #region Smartphone_BCCP_BD13_Di
        public List<SmartphoneDTO> GetListSmartphone_BCCP_BD13_Di()
        {
            try
            {
                var data = _db.GetListSP<SmartphoneDTO>("EMS_BCCP_GetList_Smartphone_BCCP_BD13_Di");
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetList_Smartphone_BCCP_BD13_Di],ex={0}", ex.Message));
                return null;
            }
        }
        public int UpdateSmartphone_BCCP_BD13_Di(long id, int trangthai)
        {
            //try
            //{
            //    List<SqlParameter> parameters = new List<SqlParameter>
            //    {
            //            new SqlParameter("@_Id", SqlDbType.BigInt) { Value = id },
            //            new SqlParameter("@_TrangThai", SqlDbType.Int) { Value = trangthai },
            //            new SqlParameter("@_ResponseCode",SqlDbType.Int) { Direction =  ParameterDirection.Output }
            //    };
            //    var data = _db.ExecuteNonQuerySP("EMS_BCCP_Smartphone_BCCP_BD13_Di_Update", parameters.ToArray());
            //    int result = -1;
            //    if(parameters["@_ResponseCode"].Value == null)
            //    {

            //    }
            //    return data;
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_GetList_Smartphone_BCCP_BD13_Di],ex={0}", ex.Message));
            //    return -999;
            //}
            try
            {
                string constring = Config.getConnectioneEnterPrise();
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("EMS_BCCP_Smartphone_BCCP_BD13_Di_Update", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@_Id", id);
                        cmd.Parameters.AddWithValue("@_TrangThai",trangthai);
                        cmd.Parameters.Add("@_ResponseCode", SqlDbType.Int);
                        cmd.Parameters["@_ResponseCode"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int result = -1;
                        if (cmd.Parameters["@_ResponseCode"].Value != null) {
                            result = Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value.ToString());
                        };
                        con.Close();
                        con.Dispose();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][EMS_BCCP_Smartphone_BCCP_BD13_Di_Update],ex={0}", ex.Message));
                return -999;
            }

        }
        #endregion
    }
}
