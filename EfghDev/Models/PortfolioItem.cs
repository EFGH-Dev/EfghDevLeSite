namespace EfghDev.Models
{
    public class PortfolioItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty; // Ex: "C#, Blazor, SQL"
        public string ImageUrl { get; set; } = string.Empty; // Optionnel si tu as des screenshots
        public string Category { get; set; } = "Autre";
    }
}