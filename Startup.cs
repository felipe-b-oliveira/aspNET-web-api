using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(webApp.Startup))]

namespace webApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c => {

                c.SingleApiVersion("v1", "webApp");
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"\bin\webApp.xml");

            });

            app.UseCors(CorsOptions.AllowAll);

            ActivatingAcessTokens(app);

            app.UseWebApi(config);
        }

        private void ActivatingAcessTokens(IAppBuilder app)
        {
            var TokensConfigurationOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"), // Endereco de acesso do Token
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1), // Tempo em que o token permanecerá ativo
                Provider = new AcessTokenProvider()
            };

            app.UseOAuthAuthorizationServer(TokensConfigurationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
