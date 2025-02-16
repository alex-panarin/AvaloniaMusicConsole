using AvaloniaMusicConsole.Data.Contents;
using AvaloniaMusicConsole.Data.Helpers;
using AvaloniaMusicConsole.Data.Interfaces;

namespace AvaloniaMusicConsole.Data
{
    public class LocalContentProvider
        : IContentProvider
    {
        public async IAsyncEnumerable<IContent> GetContents(IContent content)
        {
            var contentValue = await content.GetValue();

            if (contentValue.IsEmpty())
                yield return default!;

            await foreach(var item in content.GetValues())
            {
                yield return item;
            }
        }

        public IAsyncEnumerable<IContent> GetContents(string root)
        {
            return GetContents(new DirectoryContent(root));
        }
    }
}
