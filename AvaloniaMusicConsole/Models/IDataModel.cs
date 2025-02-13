using System;

namespace AvaloniaMusicConsole.Models
{
    public interface IDataModel
    {
        Guid Id { get; }
        string Key { get; }
    }
}
