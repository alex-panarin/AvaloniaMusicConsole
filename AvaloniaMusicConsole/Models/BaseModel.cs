using System;

namespace AvaloniaMusicConsole.Models
{
    public class BaseModel
        : IDataModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
            Key = this.GetType().Name;
        }
        public Guid Id { get; }
        public string Key { get; }
    }
}
