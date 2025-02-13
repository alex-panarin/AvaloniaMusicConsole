using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaMusicConsole.ViewModels;
using System;

namespace AvaloniaMusicConsole.Views
{
    public partial class MainWindow : Window
    {
        private readonly IClassicDesktopStyleApplicationLifetime? _desktop;

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(IClassicDesktopStyleApplicationLifetime desktop)
            : this()
        {
            _desktop = desktop;
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.SetupLifetimeViewModel(_desktop!);
            }
            base.OnDataContextChanged(e);
        }
    }
}