using Base.Util.Core8.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Base.Util.Core8.Cores.Base
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
        public static void AddSwaggerCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppSetting.GetSection("Startup.Swagger.Title"),
                    Version = AppSetting.GetSection("Startup.Swagger.Version")
                });

                #region 读取接口注释文件

                List<string> xmlFiles = AppSetting.GetSections<string>("Startup.Swagger.XmlFiles");
                if (xmlFiles.Any())
                {
                    xmlFiles.ForEach(xmlFile =>
                    {
                        if (!string.IsNullOrWhiteSpace(xmlFile))
                        {
                            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                            if (File.Exists(xmlPath))
                            {
                                options.IncludeXmlComments(xmlPath, true);
                            }
                        }
                    });
                }

                #endregion

                options.OrderActionsBy(o => o.RelativePath);// 接口展示排序
            });
        }

        /// <summary>
        /// Swagger 服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="xmlFiles"></param>
        public static void AddSwaggerCore(this IServiceCollection services, List<string> xmlFiles)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppSetting.GetSection("Startup.Swagger.Title"),
                    Version = AppSetting.GetSection("Startup.Swagger.Version")
                });

                #region 读取接口注释文件

                if (xmlFiles.Any())
                {
                    xmlFiles.ForEach(xmlFile =>
                    {
                        if (!string.IsNullOrWhiteSpace(xmlFile))
                        {
                            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                            if (File.Exists(xmlPath))
                            {
                                options.IncludeXmlComments(xmlPath, true);
                            }
                        }
                    });
                }

                #endregion

                options.OrderActionsBy(o => o.RelativePath);// 接口展示排序
            });
        }


        /// <summary>
        /// Swagger 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", AppSetting.GetSection("Startup.Swagger.Name"));
                c.RoutePrefix = AppSetting.GetSection("Startup.Swagger.RoutePrefix");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
