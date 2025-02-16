using AvaloniaMusicConsole.Data.Helpers;
using AvaloniaMusicConsole.Data.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.Models
{
    internal static class ModelFactory
    {
        public static async Task<BaseModel?> CreateModel(this IContent content)
        {
            var value = await content.GetValue();
            if (value.IsEmpty()) 
                return default;

            await Task.Yield();
            return CreateModel(JObject.Parse(value));

        }
        private static BaseModel CreateModel(JObject j)
        {
            return j.TryGetValue("IsDirectory", StringComparison.OrdinalIgnoreCase, out var token)
                && token is JValue v && ((bool?)v.Value) == true
                ? j.ToObject<Album>()!
                : j.ToObject<Track>()!;
        }
    }
}
