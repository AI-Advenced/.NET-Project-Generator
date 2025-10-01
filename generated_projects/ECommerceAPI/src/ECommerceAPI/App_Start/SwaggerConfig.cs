using System.Web.Http;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(ECommerceAPI.SwaggerConfig), "Register")]

namespace ECommerceAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ECommerceAPI");
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Documentation");
                });
        }
    }
}