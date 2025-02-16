using System.Collections.Generic;

namespace AvaloniaMusicConsole.Models
{
    public class Album 
        : BaseModel
    {
        public Album(IEnumerable<Track>? tracks)
            : base()
        {
            Tracks = tracks;
        }

        public string? Title => Name;
        public IEnumerable<Track>? Tracks { get; set; }
    }
}
