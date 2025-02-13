using AvaloniaMusicConsole.Models;
using System;

namespace AvaloniaMusicConsole.ViewModels
{
    internal class TemplateViewModelBase
        : ViewModelBase
        , IDataModel
    {
        public TemplateViewModelBase()
        {
            Id = Guid.NewGuid();
            Key = this.GetType().Name;
        }
        public Guid Id { get; }
        public string Key { get; }
    }
}
