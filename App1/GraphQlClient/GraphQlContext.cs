using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GraphQLClient
{
    public class GraphQlContext
    {
        static readonly Lazy<GraphQLHttpClient> _clientHolder = new Lazy<GraphQLHttpClient>(() => CreateGraphQLClient(new HttpClient(Xamarin.Forms.DependencyService.Get<IPentagonHackingService>().GetHttpClientHandler())));
        public static GraphQLHttpClient Client => _clientHolder.Value;
        public static GraphQLHttpClient CreateGraphQLClient(HttpClient httpClient)
        {

            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(BackendConstants.GraphQLApiUrl),
             
            }; 

            return new GraphQLHttpClient(options, new NewtonsoftJsonSerializer(), httpClient);
        }

    }
}
