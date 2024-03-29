﻿using Newtonsoft.Json;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using Shipper.RestGateway.Model;
using System;

namespace Shipper.RestGateway.BaseClient
{
    public class JsonSerializer: ISerializer, IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        public JsonSerializer()
        {
            ContentType = "application/json";
            _serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
            };
        }

        public JsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        public string Serialize(object obj)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        public string DateFormat { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string ContentType { get; set; }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;
            using (var stringReader = new System.IO.StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    try
                    {
                        return _serializer.Deserialize<T>(jsonTextReader);
                    }
                    catch (Exception)
                    {
                        return default(T);
                    }
                }
            }
        }
    }
}
