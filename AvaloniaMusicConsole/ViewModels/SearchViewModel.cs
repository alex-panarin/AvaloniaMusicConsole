using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
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

        public SearchViewModel()
        {
            Search = new RelayCommand(async () => await OnSearch());
        }

        private async Task OnSearch()
        {
            try
            {
                NotFoundText = string.Empty;
                IsNotFound = false;
                IsBusy = true;
                await Task.Delay(1000);
                // SearchResults.Clear();
                // TODO: ask repository data -> by SearchText property
            }
            finally 
            { 
                IsBusy = false;
            }
        }

        public ObservableCollection<AlbumViewModel> SearchResults { get; } = [];
        
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
    }
}
