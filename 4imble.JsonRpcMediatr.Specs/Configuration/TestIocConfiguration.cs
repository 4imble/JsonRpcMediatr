using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace _4imble.JsonRpcMediatr.Specs.Configuration
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
