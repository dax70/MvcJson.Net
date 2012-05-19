using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MvcJson.Net.Models
{
    public class JsonNetResult : JsonResult
    {
        private IContractResolver defaultContractResolver;

        public JsonNetResult()
        {
            this.defaultContractResolver = new ContractResolver { UseCamelCase = true };
            this.SerializerSettings = CreateDefaultSerializerSettings();
        }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (this.Data == null)
            {
                return;
            }
            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            Type type = this.Data.GetType();
            // Wrap Enumerables to avoid potential Json Hijacking.
            if (TypeHelper.TryWrappingForIEnumerableGenericOrSame(ref type))
            {
                this.Data = TypeHelper.GetTypeRemappingConstructor(type).Invoke(new object[] { this.Data });
            }

            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(response.Output) { CloseOutput = false })
            {
                var serializer = JsonSerializer.Create(this.SerializerSettings);
                serializer.Serialize(jsonTextWriter, this.Data);
                jsonTextWriter.Flush();
            }
        }

        private JsonSerializerSettings CreateDefaultSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = this.defaultContractResolver,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

    }
}