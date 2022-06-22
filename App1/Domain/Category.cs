using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
