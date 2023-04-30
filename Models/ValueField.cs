namespace KaizenCase.Models
{
    public class ValueField
    {
        public string? Locale { get; set; }
        public string? Description { get; set; }
        public BoundingPoly BoundingPoly { get; set; } = new BoundingPoly();
    }
}
