﻿using System.ComponentModel;

namespace Base.Util.Common.Models.WebApi
{
    /// <summary>
    /// 自定义http状态错误
    /// 对应System.Net.HttpStatusCode
    /// </summary>
    public enum HttpStateCode
    {
        /// <summary>
        /// 2XX （请求成功）表示成功处理了请求的状态代码。
        /// </summary>
        [Description("服务器已成功处理了请求。 通常，这表示服务器提供了请求的网页。")]
        成功 = 200,
        [Description("请求成功并且服务器创建了新的资源。")]
        已创建 = 201,
        [Description("服务器已接受请求，但尚未处理。")]
        已接受 = 202,
        [Description("服务器已成功处理了请求，但返回的信息可能来自另一来源。")]
        非授权信息 = 203,
        [Description("服务器成功处理了请求，但没有返回任何内容。")]
        无内容 = 204,
        [Description("服务器成功处理了请求，但没有返回任何内容。")]
        重置内容 = 205,
        [Description("服务器成功处理了部分 GET 请求。")]
        部分内容 = 206,


        /// <summary>
        /// 3XX （请求被重定向）表示要完成请求，需要进一步操作。 通常，这些状态代码用来重定向。
        /// </summary>
        [Description("针对请求，服务器可执行多种操作。 服务器可根据请求者 (user agent) 选择一项操作，或提供操作列表供请求者选择。")]
        多种选择 = 300,
        [Description("请求的网页已永久移动到新位置。 服务器返回此响应（对 GET 或 HEAD 请求的响应）时，会自动将请求者转到新位置。")]
        永久移动 = 301,
        [Description("服务器目前从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求。")]
        临时移动 = 302,
        [Description("请求者应当对不同的位置使用单独的 GET 请求来检索响应时，服务器返回此代码。")]
        查看其他位置 = 303,
        [Description("自从上次请求后，请求的网页未修改过。 服务器返回此响应时，不会返回网页内容。")]
        未修改 = 304,
        [Description("请求者只能使用代理访问请求的网页。 如果服务器返回此响应，还表示请求者应使用代理。")]
        使用代理 = 305,
        [Description("服务器目前从不同位置的网页响应请求，但请求者应继续使用原有位置来进行以后的请求。")]
        临时重定向 = 307,


        /// <summary>
        /// 4XX （请求错误）这些状态代码表示请求可能出错，妨碍了服务器的处理
        /// </summary>
        [Description("服务器不理解请求的语法。")]
        错误请求 = 400,
        [Description("请求要求身份验证。")]
        未授权 = 401,
        [Description("请求没有权限。")]
        禁止 = 403,
        [Description("服务器找不到请求的网页。")]
        未找到 = 404,
        [Description("禁用请求中指定的方法。")]
        方法禁用 = 405,
        [Description("无法使用请求的内容特性响应请求的网页。")]
        不接受 = 406,
        [Description("此状态代码与 401（未授权）类似，但指定请求者应当授权使用代理。")]
        需要代理授权 = 407,
        [Description("服务器等候请求时发生超时。")]
        请求超时 = 408,
        [Description("服务器在完成请求时发生冲突。 服务器必须在响应中包含有关冲突的信息。")]
        冲突 = 409,
        [Description("如果请求的资源已永久删除，服务器就会返回此响应。")]
        已删除 = 410,
        [Description("服务器不接受不含有效内容长度标头字段的请求。")]
        需要有效长度 = 411,
        [Description("服务器未满足请求者在请求中设置的其中一个前提条件。")]
        未满足前提条件 = 412,
        [Description("服务器无法处理请求，因为请求实体过大，超出服务器的处理能力。")]
        请求实体过大 = 413,
        [Description("请求的 URI（通常为网址）过长，服务器无法处理。")]
        请求的URI过长 = 414,
        [Description("请求的格式不受请求页面的支持。")]
        不支持的媒体类型 = 415,
        [Description("如果页面无法提供请求的范围，则服务器会返回此状态代码。")]
        请求范围不符合要求 = 416,
        [Description("服务器未满足'期望'请求标头字段的要求。")]
        未满足期望值 = 417,


        /// <summary>
        /// 5XX（服务器错误）这些状态代码表示服务器在尝试处理请求时发生内部错误。 这些错误可能是服务器本身的错误，而不是请求出错。
        /// </summary>
        [Description("服务器遇到错误，无法完成请求。")]
        服务器内部错误 = 500,
        [Description("服务器不具备完成请求的功能。 例如，服务器无法识别请求方法时可能会返回此代码。")]
        尚未实施 = 501,
        [Description("服务器作为网关或代理，从上游服务器收到无效响应。")]
        错误网关 = 502,
        [Description("服务器目前无法使用（由于超载或停机维护）。通常，这只是暂时状态。")]
        服务不可用 = 503,
        [Description("服务器作为网关或代理，但是没有及时从上游服务器收到请求。")]
        网关超时 = 504,
        [Description("服务器不支持请求中所用的 HTTP 协议版本。")]
        HTTP版本不受支持 = 505,
    }
}
