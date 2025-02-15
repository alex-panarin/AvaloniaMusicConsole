namespace AvaloniaMusicConsole.Data.Interfaces
{
    public interface IAlbumRepository
        : IContentPrivider
    {
        string Url { get; }
    }
}
