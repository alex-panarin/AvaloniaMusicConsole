using Newtonsoft.Json.Linq;

namespace AvaloniaMusicConsole.Data.Helpers
{
    internal static class Utils
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            if(action == null) throw new ArgumentNullException("action");

            var en = values.GetEnumerator();
            while (en.MoveNext()) 
                action(en.Current);
        }
    }

    public static class JUtils
    {
        public const string DIRECTORY = "directory";
        public const string FILE = "file";

        public static JObject AddIsEmpty(this JObject parent, bool val = true)
        {
            parent.Add("isEmpty", new JValue(val));
            return parent;
        }
        
        public static JObject AddParent(this JObject parent, string value)
        {
            parent.Add("parent", new JValue(value));
            return parent;
        }
        public static JObject CallDirectory(this JObject parent)
        {
            parent.Add("call", new JValue("directory"));
            return parent;
        }

        public static JObject CallFile(this JObject parent)
        {
            parent.Add("call", new JValue("file"));
            return parent;
        }

        public static JObject AddDirectory(this JObject parent, string value)
        {
            parent.Add(value, new JValue(DIRECTORY));
            return parent;
        }
        public static JObject AddFile(this JObject parent, string value)
        {
            parent.Add(value, new JValue(FILE));
            return parent;
        }
    }
}
