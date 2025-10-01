using System.Web.Http;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(BasicCrudAPI.SwaggerConfig), "Register")]

namespace BasicCrudAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "BasicCrudAPI");
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Documentation");
                });
        }
    }
}