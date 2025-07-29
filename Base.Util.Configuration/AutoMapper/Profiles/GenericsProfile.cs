using AutoMapper;
using Base.Util.Common.Models.Common;
using Base.Util.Common.Models.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Util.Configuration.AutoMapper.Profiles
{
    /// <summary>
    /// 泛型类型关系规则
    /// </summary>
    public class GenericsProfile : Profile
    {
        /// <summary>
        /// 泛型类型关系规则
        /// </summary>
        public GenericsProfile()
        {
            CreateMap(typeof(PagingList<>), typeof(PagingList<>));

            CreateMap(typeof(OperationModel<>), typeof(OperationModel<>));

            CreateMap(typeof(ServiceMessageModel<>), typeof(ServiceMessageModel<>));

        }
    }
}
