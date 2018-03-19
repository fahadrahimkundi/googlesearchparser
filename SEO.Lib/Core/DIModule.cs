using Autofac;
using SEO.Lib.Parser;

namespace SEO.Lib.Core
{
   public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register custom Classes with Interfaces in container
            builder.RegisterType<HttpManager>().As<INetworkManager>().InstancePerRequest();
            builder.RegisterType<GoogleParser>().As<IParser>().InstancePerRequest();

            base.Load(builder);
        }
    }
}