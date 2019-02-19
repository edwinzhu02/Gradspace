using System.Web;
using System.Web.Mvc;
using Jupiter.ActionFilter;

namespace Jupiter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
