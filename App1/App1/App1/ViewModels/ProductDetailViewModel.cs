using App1.Services;
using Domain;
using Domain.Entities;
using GraphQlClient;
using GraphQLClient;
using System;
using System.Diagnostics;
using System.Linq;
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
        private string _price;
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

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public string Price { get => _price; set => SetProperty(ref _price, value); }
        public decimal Rating { get => _rating; set => SetProperty(ref _rating, value); }
        public string CategoryName { get => _categoryName; set => SetProperty(ref _categoryName, value); }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var client = GraphQlContext.Client;
                var content = await client.SendQueryAsync<ResponseType>(new GraphQL.GraphQLRequest(@"
                                                                                                    query {
                                                                                                    products(where: {id: {eq: " + itemId + @"}}) {
                                                                                                    totalCount
                                                                                                    items {
                                                                                                    id
                                                                                                    attributeIdValue {
                                                                                                    id, value
                                                                                                    }
                                                                                                    name,
                                                                                                    category {
                                                                                                    id, name, properties {
                                                                                                    id, name
                                                                                                    }
                                                                                                    }
                                                                                                    price
                                                                                                    rating
                                                                                                    image {
                                                                                                    id
                                                                                                    imagePath
                                                                                                    }
                                                                                                    reviews {
                                                                                                    id, user {
                                                                                                    id
                                                                                                    } content, rating, date
                                                                                                    }, description
                                                                                                    }
                                                                                                    }
                                                                                                    }")).ConfigureAwait(false);
                var item = content.Data.Products.Items.FirstOrDefault();
                Id = item.Id;
                Name = item.Name;
                Description = item.Description;
                Price = item.Price.ToString();
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
