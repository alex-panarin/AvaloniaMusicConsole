using AvaloniaMusicConsole.Models;
using System.Collections.ObjectModel;

namespace AvaloniaMusicConsole.ViewModels
{
    public class AlbumViewModel
        : TemplateViewModelBase
    {
        public ObservableCollection<Album> Albums { get; set; } = new (new []{new Album(), new Album()});
    }
}
