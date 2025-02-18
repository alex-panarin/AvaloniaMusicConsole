using AvaloniaMusicConsole.Data.Interfaces;
using AvaloniaMusicConsole.Models;
using System;
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
            this.contentProvider = contentProvider 
                ?? throw new ArgumentNullException(nameof(contentProvider));
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
                var model = await content.CreateModel(); 
                if(model != null)
                    queue.Enqueue(model);
            }
            while (queue.TryDequeue(out var model))
            {
                yield return model;
            }
        }
    }
}
