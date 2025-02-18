using AvaloniaMusicConsole.Models;
using System;
using System.Threading.Tasks;

namespace AvaloniaMusicConsole.ViewModels
{
    public abstract class TemplateViewModelBase<TModel>
        : ViewModelBase
        , IDataModel
        where TModel : BaseModel 
    {
        public TemplateViewModelBase(TModel model)
        {
            Id = Guid.NewGuid();
            Key = this.GetType().Name;
            Model = model;
        }
        public Guid Id { get; }
        public string Key { get; }
        protected TModel Model { get; private set; }
        public abstract Task LoadDataAsync();
        protected abstract TemplateViewModelBase<TModel> CreateViewModel(TModel model);
    }
}
