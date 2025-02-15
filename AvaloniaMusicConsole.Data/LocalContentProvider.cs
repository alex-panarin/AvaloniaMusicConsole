using AvaloniaMusicConsole.Data.Interfaces;

namespace AvaloniaMusicConsole.Player
{
    internal class LocalContentProvider
        : IContentPrivider
    {

        public Task<IContent?> GetContent(IContent content)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IContent>> GetContents()
        {
            throw new NotImplementedException();
        }
    }
}
