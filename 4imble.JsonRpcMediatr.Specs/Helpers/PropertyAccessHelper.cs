using System;

namespace _4imble.JsonRpcMediatr.Specs.Helpers
{
    public class PropertyAccessHelper
    {
        public static T TryGetValue<T>(Func<T> getPropertyValue)
        {
            try
            {
                return getPropertyValue();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T TryGetEnumValue<T>(string enumName)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), enumName);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}