using Newtonsoft.Json;

namespace _4imble.Mediatr.JsonRpc.Specs.Helpers
{
    public static class SerializationHelper
    {
        public static T ReserializeObject<T>(T item)
        {
            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(item));
        }
    }
}
