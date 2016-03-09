using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Projeto.Web.Startup))]

namespace Projeto.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigurationIdentity(app);
        }

        //método para configurar o Asp.Net Identity
        public void ConfigurationIdentity(IAppBuilder app)
        {
            //Autenticação por autorização cookie gerada neste projeto..
            app.UseCookieAuthentication(
                    new CookieAuthenticationOptions() {
                        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                        LoginPath = new PathString("/Usuario/Login")                  
                    }
                );
        }
    }
}
