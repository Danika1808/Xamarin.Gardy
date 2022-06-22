using App1.Services;
using Domain;
using GraphQLClient;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ProductDetailViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private decimal _price;
        private decimal _rating;
        private string _categoryName;

        private int _itemId;
        public int Id { get; set; }

        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public decimal Price { get => _price; set => _price = value; }
        public decimal Rating { get => _rating; set => _rating = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var client = GraphQlContext.Client;
                var content = await client.SendQueryAsync<Product>(new GraphQL.GraphQLRequest(@"query{
                                                                                                      products(where: {id: {eq: " + itemId + @"}})
                                                                                                      {
                                                                                                      totalCount,
                                                                                                      items{
                                                                                                      id, name, price, rating, image { id, imagePath
                                                                                                      }, attributeValuePairs { key, value }
                                                                                                      }
                                                                                                      }
                                                                                                      }")).ConfigureAwait(false);
                var item = content.Data;
                Id = item.Id;
                Name = item.Name;
                Description = item.Description;
                Price = item.Price;
                Rating = item.Rating;
                CategoryName = item.Category.Name;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
