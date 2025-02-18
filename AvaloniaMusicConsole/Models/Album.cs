using System.IO;

namespace AvaloniaMusicConsole.Models
{
    public class Album 
        : BaseModel
    {
        public Album()
            : base()
        {
            IsDirectory = true;
        }

        public string? Title => Name;
        
    }
}
