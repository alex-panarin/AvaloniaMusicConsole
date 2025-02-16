using AvaloniaMusicConsole.Data.Interfaces;

namespace AvaloniaMusicConsole.Data.Contents
{
    public abstract class Content
        : IContent
    {
        protected Content(string url)
        {
            Url = url;
        }

        public virtual string? Name => Url;
        public virtual string RootPath => Url;

        public Task<string> GetValue() => GetContentAsync(); 

        protected abstract Task<string> GetContentAsync();

        protected string Url { get; }
        
        public IAsyncEnumerable<IContent> GetValues() => GetContentsAsync();

        protected abstract IAsyncEnumerable<IContent> GetContentsAsync();
        
    }
}
