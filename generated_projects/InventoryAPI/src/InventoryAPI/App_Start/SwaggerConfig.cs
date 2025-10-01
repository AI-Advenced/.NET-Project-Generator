using System.Web.Http;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(InventoryAPI.SwaggerConfig), "Register")]

namespace InventoryAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "InventoryAPI");
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Documentation");
                });
        }
    }
}