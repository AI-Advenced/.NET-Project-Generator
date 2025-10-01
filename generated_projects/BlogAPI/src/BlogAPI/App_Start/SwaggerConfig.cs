using System.Web.Http;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(BlogAPI.SwaggerConfig), "Register")]

namespace BlogAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "BlogAPI");
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Documentation");
                });
        }
    }
}