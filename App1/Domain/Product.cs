using System.Collections.Generic;

namespace Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public Category Category { get; set; }
        public int ImageId { get; set; }
        public ImageMetadata Image { get; set; }
        public Dictionary<string,string> AttributeValuePairs { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
