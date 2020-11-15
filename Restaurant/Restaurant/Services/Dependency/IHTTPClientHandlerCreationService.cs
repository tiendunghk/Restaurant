using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Restaurant.Services.Dependency
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
