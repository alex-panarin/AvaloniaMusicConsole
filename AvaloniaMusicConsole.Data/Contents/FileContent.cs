using AvaloniaMusicConsole.Data.Interfaces;
using Newtonsoft.Json.Linq;

namespace AvaloniaMusicConsole.Data.Contents
{
    public class FileContent
        : Content
    {
        public FileContent(string url)
            : base(url)
        {
            
        }
        public bool IsDirectory => false;
        public override string? Name => Path.GetFileName(Url);
        public override string RootPath => Path.GetDirectoryName(Url)!;
        protected override async Task<string> GetContentAsync()
        {
            if(File.Exists(Url) == false) return string.Empty;

            return await Task.FromResult(JObject.FromObject(this).ToString());
        }

        protected override async IAsyncEnumerable<IContent> GetContentsAsync()
        {
            await Task.Yield();
            yield return default!;
        }
    }
}
