using System.Web;

namespace Utils
{
    public class AccountUtils
    {
        const string accountId = "st_accountId";
        const string accountName = "st_accountName";
        const string loop = "st_loop";
        public static long AccountId
        {
            get
            {
                var stAccountId = HttpContext.Current.Session[accountId];
                if (stAccountId == null)
                {
                    return -1;
                }
                long accId;
                long.TryParse(stAccountId.ToString(), out accId);
                if (accId > 0)
                {
                    return accId;
                }
                return -1;

            }
            set
            {
                HttpContext.Current.Session[accountId] = value;
            }
        }
        public static bool Loop
        {
            get
            {
                var stloop = HttpContext.Current.Session[loop];
                if (stloop == null)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            set
            {
                HttpContext.Current.Session[loop] = value;
            }
        }
    }
}
