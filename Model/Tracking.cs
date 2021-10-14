namespace EcommerceVT.Model
{
    public class Tracking : Shipping
    {
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
    }
}
