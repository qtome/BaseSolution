﻿using Base.AuthorityApi.Controller.Base;
using Base.Util.Core8.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Base.AuthorityApi.Controller
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    public class TestsController : BaseAuthorityController
    {
        public TestsController() { }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> TestMethod() 
        {
            try
            {
                var result = true;
                return result;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
    }
}
