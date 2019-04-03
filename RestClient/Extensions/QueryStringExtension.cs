using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TCE.RestClient.Extensions
{
    public static class QueryStringExtension
    {
        public static async Task<Uri> ToUriAsync(this string query, object request)
        {
            var keyValueContent = request.ToKeyValue();
            using (var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent))
            {
                var urlEncodedString = await formUrlEncodedContent.ReadAsStringAsync();
                var uriBuilder = new UriBuilder(query);
                uriBuilder.Query = urlEncodedString;
                return uriBuilder.Uri;
            }
        }
        
        public static Uri ToUri(this string query, object request)
        {            
            var keyValueContent = request.ToKeyValue();
            var uriBuilder = new UriBuilder(query);
            if (keyValueContent != null)
            {
                using (var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent))
                {
                    var urlEncodedString = formUrlEncodedContent.ReadAsStringAsync().Result;                    
                    uriBuilder.Query = urlEncodedString;                    
                }
            }
            return uriBuilder.Uri;
        }

        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData
                                        .Concat(childContent)
                                        .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                            jValue?.ToString("o", CultureInfo.InvariantCulture) :
                            jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }

    }
}
