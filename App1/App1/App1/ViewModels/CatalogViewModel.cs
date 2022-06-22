using App1.Views;
using System;
using Domain;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using GraphQLClient;
using GraphQL;
using System.Collections.Generic;
using App1.Services;
using System.Net.Http;

namespace App1.ViewModels
{
    public class CatalogViewModel : BaseViewModel
    {
        private Product _selectedItem;

        public ObservableCollection<Product> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Product> ItemTapped { get; }

        public CatalogViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Product>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var client = GraphQlContext.Client;

                var graphQLRequest = new GraphQLRequest
                {
                    Query = @"
                            query {
                            products {
                            totalCount
                            items {
                            id
                            name
                            price
                            rating
                            image {
                            id
                            imagePath
                            }
                            attributeValuePairs {
                            key
                            value
                            }
                            }
                            }
                            }"
                };

                var products = await client.SendQueryAsync<List<Product>>(graphQLRequest).ConfigureAwait(false);

                foreach (var item in products.Data)
                {
                    Console.WriteLine(item);
                }
                var items = products.Data;
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Product item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ProductDetailViewModel.ItemId)}={item.Id}");
        }
    }
}