using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiConnectOracle.Models
{
    public class E1I2
    {
        public DataSet GetListItem(int MABC_KT, int MABCNHAN, int LOAI, string LOAIDICHVU, int NGAY, int CHTHU)
        {
            try
            {
                E1I2DA myE1I2DA = new E1I2DA();
                return myE1I2DA.GetListItem(MABC_KT, MABCNHAN, LOAI, LOAIDICHVU, NGAY, CHTHU);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        public DataSet GetListMailTrip(int MABC_KT, int MABCNHAN, int LOAI, string LOAIDICHVU, int NGAY, int CHTHU)
        {
            try
            {
                E1I2DA myE1I2DA = new E1I2DA();
                return myE1I2DA.GetListMailTrip(MABC_KT, MABCNHAN, LOAI, LOAIDICHVU, NGAY, CHTHU);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        public DataSet GetListPostBag(int MABC_KT, int MABCNHAN, int LOAI, string LOAIDICHVU, int NGAY, int CHTHU)
        {
            try
            {
                E1I2DA myE1I2DA = new E1I2DA();
                return myE1I2DA.GetListPostBag(MABC_KT, MABCNHAN, LOAI, LOAIDICHVU, NGAY, CHTHU);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        public DataSet GetListDispatch(int MABC_KT, int MABCNHAN, int LOAI, string LOAIDICHVU, int NGAY, int CHTHU)
        {
            try
            {
                E1I2DA myE1I2DA = new E1I2DA();
                return myE1I2DA.GetListDispatch(MABC_KT, MABCNHAN, LOAI, LOAIDICHVU, NGAY, CHTHU);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        //public DataSet GetListItem_Den(int MABC_KT, int MABCNHAN, int LOAI, string LOAIDICHVU, int NGAY, int CHTHU)
        //{
        //    try
        //    {
        //        E1E2DA myE1E2DA = new E1E2DA();
        //        return myE1E2DA.GetListItem_Den(MABC_KT, MABCNHAN, LOAI, LOAIDICHVU, NGAY, CHTHU);
        //    }
        //    catch (System.Exception e)
        //    {
        //        return null;
        //    }
        //}
    }
}