using AvaloniaMusicConsole.Models;
using AvaloniaMusicConsole.Repositories;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.ViewModels
{
    public class AlbumViewModel
        : TemplateViewModelBase
    {
        private readonly IDataRepository repository;
        private readonly string path;

        public AlbumViewModel(IDataRepository repository, string path)
        {
            this.repository = repository;
            this.path = path;
        }
        public ObservableCollectionEx<Album> Albums { get; set; } = [];// = new (new []{new Album(), new Album()});

        public async Task LoadDataAsync()
        {
            using (Albums.LockChangedEvent())
            {
                Albums.Clear();

                await foreach (var item in repository.GetModels(path))
                {
                    if (item is Album album)
                        Albums.Add(album);
                }
            }
        }
    }

}
