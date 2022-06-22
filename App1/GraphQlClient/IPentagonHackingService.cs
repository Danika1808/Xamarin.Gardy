using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GraphQLClient
{
    public interface IPentagonHackingService
    {
        HttpClientHandler GetHttpClientHandler();
    }
}
