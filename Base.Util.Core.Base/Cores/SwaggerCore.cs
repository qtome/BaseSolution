using Base.Util.Core.Base.Models.Options;
using Base.Util.Core.Base.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Base.Util.Core.Base.Cores
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerCore
    {
        /// <summary>
        /// Swagger 服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSwaggerCore(this IServiceCollection services, SwaggerCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = AppSetting.GetConfiguration<SwaggerCoreOptions>("Startup.Swagger");

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = options.Title,
                    Version = options.Version
                });

                #region 读取接口注释文件

                if (options.XmlFiles.Any())
                {
                    options.XmlFiles.ForEach(xmlFile =>
                    {
                        if (!string.IsNullOrWhiteSpace(xmlFile))
                        {
                            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                            if (File.Exists(xmlPath))
                            {
                                opt.IncludeXmlComments(xmlPath, true);
                            }
                        }
                    });
                }

                #endregion

                opt.OrderActionsBy(o => o.RelativePath);// 接口展示排序
            });
        }


        /// <summary>
        /// Swagger 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseSwaggerCore(this IApplicationBuilder app, SwaggerCoreOptions? options = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (options == null) options = AppSetting.GetConfiguration<SwaggerCoreOptions>("Startup.Swagger");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(options.Url, options.Name);
                c.RoutePrefix = options.RoutePrefix;
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
