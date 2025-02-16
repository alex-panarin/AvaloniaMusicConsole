using System;

namespace AvaloniaMusicConsole.Models
{
    public enum ModelType
    {
        Track,
        Album,
        Artist
    }
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
        public string Name { get; set; }
        public string RootPath { get; set; }
        public bool IsDirectory { get; set; }
        public override string ToString()
            => $"{Key} : {Name} : {RootPath} : {IsDirectory}";
        
    }
}
