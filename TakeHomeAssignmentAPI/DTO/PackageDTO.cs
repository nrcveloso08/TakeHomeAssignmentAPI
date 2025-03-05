namespace TakeHomeAssignmentAPI.DTO
{
    public class PackageDTO
    {
        public string Material { get; set; }
        public string Dimensions { get; set; }
        public string Type { get; set; }
        public double Weight { get; set; }
        public int? ParentPackagingId { get; set; }
    }
}
