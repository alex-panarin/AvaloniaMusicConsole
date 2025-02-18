using AvaloniaMusicConsole.Models;

namespace AvaloniaMusicConsole.ViewModels
{
    internal class TrackViewModel
        : ViewModelBase
    {

        public ObservableCollectionEx<Track> Tracks { get; } = [];
    }
}
