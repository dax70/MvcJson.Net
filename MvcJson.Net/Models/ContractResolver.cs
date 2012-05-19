using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace MvcJson.Net.Models
{
    public class ContractResolver: DefaultContractResolver
    {
        public ContractResolver()
        {
        }

        public bool UseCamelCase { get; set; }

        protected override string ResolvePropertyName(string propertyName)
        {
            return UseCamelCase ? ToCamelCase(propertyName) : propertyName;
        }

        private static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            if (!char.IsUpper(s[0]))
            {
                return s;
            }
            string camelCase = char.ToLower(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            if (s.Length > 1)
            {
                camelCase += s.Substring(1);
            }
            return camelCase;
        }

    }
}