using Base.Util.Core8.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Base.AuthorityApi.Controller.Base
{
    /// <summary>
    /// 权限基类控制器
    /// </summary>
    [Route("Base/AuthorityApp/[controller]")]
    public class BaseAuthorityController : BaseController
    {
        public BaseAuthorityController()
            : base()
        { }
    }
}
