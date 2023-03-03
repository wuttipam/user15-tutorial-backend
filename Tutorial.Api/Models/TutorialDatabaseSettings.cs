namespace Tutorial.Api.Models
{
    public class TutorialDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TutorialCollectionName { get; set; } = null!;
    }
}
