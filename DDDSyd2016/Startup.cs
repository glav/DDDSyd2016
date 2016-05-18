using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using DDDSyd2016.IdentityServer.IdentityServerConfig;

[assembly: OwinStartup(typeof(DDDSyd2016.Startup))]

namespace DDDSyd2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseIdentityServerSimpleSetup();
            //app.UseIdentityServerCustomViewSetup();
            app.UseIdentityServerCustomStoreSetup();
        }
    }
}
