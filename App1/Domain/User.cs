using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public int ImageId { get; set; }
        public ImageMetadata ImageMetadata { get; set; }
        public decimal TotalPayment { get; set; }
        public ICollection<ShoppingCartEntry> Basket { get; set; }
        public bool IsConfirmed { get; set; } 
    }
}
