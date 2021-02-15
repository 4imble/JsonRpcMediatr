using _4imble.JsonRpcMediatr.Test.Configuration;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace _4imble.JsonRpcMediatr.Tests.Configuration
{
    public class TestIocConfiguration
    {
        static Container container;

        public static void RegisterCommonConfiguration()
        {
            container = ApplicationContext.Container;

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.AllowOverridingRegistrations = true;
        }
    }
}
