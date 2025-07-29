using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Util.Configuration.Filters
{
    /// <summary>
    /// 反向权限验证过滤器（与AuthenticationFilterAttribute配对，用来过滤）
    /// </summary>
    public class NoAuthenticationFilterAttribute : ActionFilterAttribute
    {
    }
}
