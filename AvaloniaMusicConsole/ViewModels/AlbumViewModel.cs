using Avalonia.Threading;
using AvaloniaMusicConsole.Models;
using AvaloniaMusicConsole.Repositories;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.ViewModels
{
    public class AlbumViewModel
        : TemplateViewModelBase<Album>
    {
        private readonly IDataRepository repository;
        private AlbumViewModel _SelectedAlbum;

        public AlbumViewModel(Album root, IDataRepository repository)
            : base(root)
        {
            this.repository = repository;
            SelectionChange = new RelayCommand<ViewModelBase>(OnSelection);
        }
                
        public string? Title
        {
            get => Model.Title;
        }
        public ObservableCollectionEx<AlbumViewModel> Albums { get; set; } = [];
        public ObservableCollectionEx<TrackViewModel> Tracks{ get; set; } = [];

        public RelayCommand<ViewModelBase> SelectionChange { get; } 
        private void OnSelection(ViewModelBase? model)
        {
            if(model is AlbumViewModel album)
                SelectedAlbum = album;
        }

        public AlbumViewModel SelectedAlbum 
        { 
            get => _SelectedAlbum; 
            set
            {
                var newAlbum = value;
                if (newAlbum != null)
                {
                    Dispatcher.UIThread.Invoke(async () =>
                    {
                        await newAlbum.LoadDataAsync();
                    });
                }

                SetProperty(ref _SelectedAlbum, value);
            }
        }

        public override async Task LoadDataAsync()
        {
            using (Albums.LockChangedEvent())
            {
                using (Tracks.LockChangedEvent())
                {
                    Albums.Clear();
                    Tracks.Clear();

                    await foreach (var item in repository.GetModels(Model.FullPath))
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
