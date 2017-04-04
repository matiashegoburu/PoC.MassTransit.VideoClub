namespace VideoClub.Messages.Titles
{
    public interface CreateTitleMessage
    {
        string Title { get; }
        string Description { get; }
        string Category { get; }
    }

    public class CreateTitleCommand : CreateTitleMessage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
