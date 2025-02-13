namespace AvaloniaMusicConsole.Models
{
    public class Album 
        : BaseModel
    {
        public Album()
            : base()
        {
            Title = $"{Id}"[..10];
        }
        public string? Title { get; private set; } 
    }
}
