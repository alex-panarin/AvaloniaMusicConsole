namespace AvaloniaMusicConsole.Data.Interfaces
{
    public interface IContentProvider
    {
        IAsyncEnumerable<IContent> GetContents(IContent content);
        IAsyncEnumerable<IContent> GetContents(string root);
    }

    public interface IContent
    {
        Task<string> GetValue();
        IAsyncEnumerable<IContent> GetValues();
    }
}
