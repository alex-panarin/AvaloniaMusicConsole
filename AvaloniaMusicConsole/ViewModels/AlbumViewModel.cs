using Avalonia.Rendering.Composition;
using AvaloniaMusicConsole.Models;
using AvaloniaMusicConsole.Repositories;
using System.Reflection;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.ViewModels
{
    public class AlbumViewModel
        : TemplateViewModelBase<Album>
    {
        private readonly IDataRepository repository;

        public AlbumViewModel(Album root, IDataRepository repository)
            : base(root)
        {
            this.repository = repository;
        }

        public string? Title
        {
            get => Model.Title;
        }
        public ObservableCollectionEx<AlbumViewModel> Albums { get; set; } = [];
        public ObservableCollectionEx<TrackViewModel> Tracks{ get; set; } = [];

        public override async Task LoadDataAsync()
        {
            using (Albums.LockChangedEvent())
            {
                using (Tracks.LockChangedEvent())
                {
                    Albums.Clear();
                    Tracks.Clear();

                    await foreach (var item in repository.GetModels(Model.Name))
                    {
                        if (item is Album album)
                            Albums.Add(CreateViewModel(album));
                        else if (item is Track track)
                            Tracks.Add(CreateViewModel(track));
                    }
                }
            }
        }

        protected TrackViewModel CreateViewModel(Track model)
            => new TrackViewModel(model, repository);

        protected override AlbumViewModel CreateViewModel(Album model)
            => new AlbumViewModel(model, repository);
        
    }

}
