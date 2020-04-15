using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace _4imble.JsonRpcMediatr.Specs.Configuration
{
    public static class ApplicationContext
    {
        public static readonly Container Container;

        static ApplicationContext()
        {
            Container = new Container();
        }

        public static T Resolve<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
