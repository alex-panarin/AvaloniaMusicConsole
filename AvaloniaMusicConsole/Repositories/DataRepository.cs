using AvaloniaMusicConsole.Data.Interfaces;
using AvaloniaMusicConsole.Models;
using System.Collections.Generic;

namespace AvaloniaMusicConsole.Repositories
{
    public interface IDataRepository
    {
        IAsyncEnumerable<BaseModel?> GetModels(string path);
        IAsyncEnumerable<BaseModel?> GetModelsBuffering(string path);
    }

    public class DataRepository
        : IDataRepository
    {
        private readonly IContentProvider contentProvider;

        public DataRepository(IContentProvider contentProvider) 
        {
            this.contentProvider = contentProvider;
        }

        public async IAsyncEnumerable<BaseModel?> GetModels(string path)
        {
            await foreach(IContent content in contentProvider.GetContents(path))
            {
                yield return await content.CreateModel();
            }
        }

        public async IAsyncEnumerable<BaseModel?> GetModelsBuffering(string path)
        {
            var queue = new Queue<BaseModel>();

            await foreach (IContent content in contentProvider.GetContents(path))
            {
                var item = await content.CreateModel(); 
                if(item != null)
                    queue.Enqueue(item);
            }
            while (queue.Count > 0)
            {
                yield return queue.Dequeue();
            }
        }
    }
}
