using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace _4imble.Mediatr.JsonRpc.Specs.Helpers
{
    public static class TestContext
    {
        static readonly IDictionary<string, object> TestBag = new ExpandoObject();

        public static void Remember<T>(string name, T value)
        {
            TestBag[name] = value;
        }

        public static T Recall<T>(string name)
        {
            return (T)TestBag[name];
        }

        public static T RecallLatest<T>()
        {
            return TestBag.Select(x => x.Value).OfType<T>().Last();
        }

        public static void Reset()
        {
            ((IDictionary<string, object>)TestBag).Clear();
        }
    }
}
