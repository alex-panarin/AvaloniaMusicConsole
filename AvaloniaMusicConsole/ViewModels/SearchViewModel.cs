using AvaloniaMusicConsole.Data;
using AvaloniaMusicConsole.Models;
using AvaloniaMusicConsole.Repositories;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.ViewModels
{
    public partial class SearchViewModel
        : ViewModelBase
    {
        private string? _searchText;
        private bool _isBusy;
        private string? _notFoundText;
        private bool _isNotFound;
        private ModelType _selectedModelType;
        private AlbumViewModel _templateViewModel;

        public SearchViewModel()
        {
            Search = new RelayCommand(async () => await OnSearch());
            SelectedModelType = ModelType.Album;
            RootPath = @"E:\";
            RootAlbum = new Album() {RootPath = RootPath, Name= "Music" };
            _templateViewModel = new AlbumViewModel(RootAlbum, new DataRepository(new LocalContentProvider()));
        }

        private async Task OnSearch()
        {
            try
            {
                NotFoundText = string.Empty;
                IsNotFound = false;
                IsBusy = true;
                //await Task.Delay(1000);
                SearchResults.Clear();

                await AlbumViewModel.LoadDataAsync();

                //TemplateViewModel = album;

            }
            finally
            {
                IsBusy = false;
            }
            await Task.CompletedTask;
        }
        public string RootPath { get; set; }
        public Album RootAlbum { get; private set; }
        public AlbumViewModel AlbumViewModel
        {
            get => _templateViewModel;
            set => this.SetProperty(ref _templateViewModel, value); 
        }
        public ObservableCollectionEx<IDataModel> SearchResults { get; } = [];
        
        public string? SearchText
        {
            get => _searchText;
            set => this.SetProperty(ref _searchText, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => this.SetProperty(ref _isBusy, value);
        }

        public string? NotFoundText
        {
            get => _notFoundText;
            set => this.SetProperty(ref _notFoundText, value);
        }

        public bool IsNotFound 
        { 
            get => _isNotFound; 
            set => this.SetProperty(ref _isNotFound, value); 
        }
        public RelayCommand? Search { get; private set; }

        public ObservableCollection<ModelType> ModelTypes { get; } = new (Enum.GetValues<ModelType>());

        public ModelType SelectedModelType 
        {
            get => _selectedModelType;
            set => this.SetProperty(ref _selectedModelType, value); 
        }
    }
}
