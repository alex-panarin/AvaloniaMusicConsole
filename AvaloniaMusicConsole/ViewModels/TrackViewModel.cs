using AvaloniaMusicConsole.Models;
using AvaloniaMusicConsole.Repositories;
using System;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.ViewModels
{
    public class TrackViewModel
        : TemplateViewModelBase<Track>
    {
        private readonly IDataRepository repository;

        public TrackViewModel(Track track, IDataRepository repository)
            : base(track)
        {
            this.repository = repository;
        }

        public override Task LoadDataAsync()
        {
            throw new NotImplementedException();
        }

        protected override TrackViewModel CreateViewModel(Track model)
            => new TrackViewModel(model, repository);
    }
}
