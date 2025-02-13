using Avalonia.Controls.ApplicationLifetimes;

namespace AvaloniaMusicConsole.ViewModels
{
    public partial class MainWindowViewModel 
        : ViewModelBase
    {
        public AppLifetimeViewModel? AppViewModel { get; set; }

        public void SetupLifetimeViewModel(IClassicDesktopStyleApplicationLifetime desktop)
            => AppViewModel = new AppLifetimeViewModel(desktop);

        public SearchViewModel SearchViewModel { get; set; } = new SearchViewModel();
    }
}
