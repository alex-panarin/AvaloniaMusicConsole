using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaMusicConsole.ViewModels
{
    public partial class AppLifetimeViewModel
        : ViewModelBase
    {
        public AppLifetimeViewModel()
        {
            Close = new RelayCommand(() => Desktop?.Shutdown());
            
            Minimize = new RelayCommand(() => Desktop!.MainWindow!.WindowState = WindowState.Minimized);

            Maximize = new RelayCommand(() => Desktop!.MainWindow!.WindowState =
                Desktop!.MainWindow!.WindowState != WindowState.Maximized 
                ? WindowState.Maximized
                : WindowState.Normal);
        }
        public AppLifetimeViewModel(IClassicDesktopStyleApplicationLifetime desktop)
            : this()
        {
            Desktop = desktop;
        }

        private IClassicDesktopStyleApplicationLifetime? Desktop { get; set; }

        public RelayCommand? Close { get; private set; } 
        public RelayCommand? Minimize { get; private set; }
        public RelayCommand? Maximize { get; private set; }

    }
}
