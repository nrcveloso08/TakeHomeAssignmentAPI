namespace TakeHomeAssignmentAPI.DTO
{
    public class ProductWithPackagingDto
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int PackagingId { get; set; }
        public string Type { get; set; }
        public string Dimensions { get; set; }
        public double Weight { get; set; }
        public int? ParentPackagingId { get; set; }
    }
}
