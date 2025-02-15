namespace AvaloniaMusicConsole.Data.Interfaces
{
    public interface IContentPrivider
    {
        Task<IContent?> GetContent(IContent content);
        Task<IEnumerable<IContent>> GetContents();
    }

    public interface IContent
    {
        Task<string> GetValue();
        Task<IEnumerable<IContent>> GetValues();
    }
}
