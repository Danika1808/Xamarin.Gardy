using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlClient
{
    public class ResponseType
    {
        public Value Products { get; set; }
    }

    public class Value
    {
        public int TotalCount { get; set; }
        public ICollection<Product> Items { get; set; }
    }
}
