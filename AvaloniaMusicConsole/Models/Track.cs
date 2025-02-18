using System.IO;

namespace AvaloniaMusicConsole.Models
{
    public class Track
        : BaseModel
    {
        public Track()
        {
            IsDirectory = false;
        }
        public string? Title => Path.GetFileNameWithoutExtension(Name);
        public string? Extension => Path.GetExtension(Name);
    }
}
