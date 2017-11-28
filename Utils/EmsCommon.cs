using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class EmsCommon
    {
        public static DateTime ConvertIntToDate(Int32 dtInput)
        {
            try
            {
                if (dtInput > 19000101)
                {
                    string strDate;
                    strDate = dtInput.ToString("00000000");
                    return new DateTime(Convert.ToInt32(strDate.Substring(0, 4)),
                        Convert.ToInt32(strDate.Substring(4, 2)),
                        Convert.ToInt32(strDate.Substring(6, 2)));
                }
                else
                {
                    return new DateTime(1900, 1, 1);
                }
            }
            catch (System.Exception e)
            {
                return new DateTime(1900, 1, 1);
            }

        }
    }
}
