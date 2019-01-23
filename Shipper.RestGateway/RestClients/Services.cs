using System;
using Polly;
using RestSharp;
using RestSharp.Deserializers;
using Shipper.RestGateway.BaseClient;
using Shipper.RestGateway.Extensions;
using Shipper.RestGateway.Helper;

namespace Shipper.RestGateway.RestClients
{
    public class Services : RestBaseClient
    {
        private static readonly Policy CircuitBreakerPolicy = Policy
           .Handle<Exception>()
           .CircuitBreaker(
               2,
               TimeSpan.FromSeconds(5)
           );

        public Services(ICacheService cache, IDeserializer serializer) : base(cache, serializer,
                                                                                               $"{WebConfigHelper.GetServicesEndpointKey}")
        {
        }

        public T GetResult<T>(string routeQuery) where T : new() {
            var request = new RestRequest(routeQuery, Method.GET);
            return CircuitBreakerPolicy.Execute(() => Get<T>(request));
        }

        public string Post<T>(T model, string route)
        {
            var request = new RestRequest(route, Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json; charset=utf-8", model.ToJson(), ParameterType.RequestBody);
            var jsonResult = CircuitBreakerPolicy.Execute(
                () => GetJsonResult(request));
            return jsonResult;
        }
    }
}
