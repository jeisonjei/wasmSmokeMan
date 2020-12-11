using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace wasmSmokeMan.Client
{
    public static class DebuggingExtensions
    {
        public static string ToJson(this object obj)
        {
            var props = GetProperties(obj);
            string json = JsonConvert.SerializeObject(obj);
            return JValue.Parse(json).ToString(Formatting.Indented);
        }
        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }
    }
}
