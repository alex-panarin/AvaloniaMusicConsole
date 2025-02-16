using AvaloniaMusicConsole.Data.Interfaces;
using Newtonsoft.Json.Linq;

namespace AvaloniaMusicConsole.Data.Contents
{
    public class DirectoryContent
        : Content
    {
        public DirectoryContent(string url)
            : base(url)
        {
        }

        public bool IsDirectory => true;
        public override string RootPath => Path.GetDirectoryName(Url)!;
        public override string Name => Path.GetFileName(Url);
        protected override async Task<string> GetContentAsync()
        {
            if (Directory.Exists(Url) == false)
            {
                return string.Empty;
            }
           
            return await Task.FromResult( JObject.FromObject(this).ToString()); 
        }

        protected override async IAsyncEnumerable<IContent> GetContentsAsync()
        {
            foreach (var path in Directory.EnumerateFileSystemEntries(Url, "*.*", SearchOption.TopDirectoryOnly))
            {
                await Task.Yield();
                yield return File.GetAttributes(path).HasFlag(FileAttributes.Directory)
                     ? new DirectoryContent(path)
                     : new FileContent(path);
            }
        }
    }
}
