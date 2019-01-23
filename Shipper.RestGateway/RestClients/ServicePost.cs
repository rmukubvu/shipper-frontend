using System;
using Polly;
using RestSharp;
using RestSharp.Deserializers;
using Shipper.RestGateway.BaseClient;
using Shipper.RestGateway.Extensions;
using Shipper.RestGateway.Helper;

namespace Shipper.RestGateway.RestClients
{
    public class ServicePost : RestBaseClient
    {
        private readonly Policy _circuitBreakerPolicy = Policy
           .Handle<Exception>()
           .CircuitBreaker(
               2,
               TimeSpan.FromSeconds(5)
           );

        public ServicePost(ICacheService cache, IDeserializer serializer) : base(cache, serializer,
                                                                                               $"{WebConfigHelper.GetServicesEndpointKey}")
        {
        }

        public string Post<T>(T model,string route){
            var request = new RestRequest(route, Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json; charset=utf-8", model.ToJson(), ParameterType.RequestBody);
            var jsonResult = _circuitBreakerPolicy.Execute(
                () => GetJsonResult(request));
            return jsonResult;
        }
    }
}
