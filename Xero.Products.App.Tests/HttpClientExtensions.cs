using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using Xero.Products.App.Http;

namespace Xero.Products.App.Tests
{
    public static class HttpClientExtensions
    {
        public static void SetAuthorizationHeader(this HttpClient client, string authorizationToken,
            string schema = "bearer")
        {
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                authorizationToken = authorizationToken.Replace("\"", "");
                authorizationToken = authorizationToken.Trim();
            }
            if (!string.IsNullOrEmpty(authorizationToken) && authorizationToken.ToLower().IndexOf(schema.ToLower()) == 0)
            {
                authorizationToken = authorizationToken.Substring(schema.Length);
                authorizationToken = authorizationToken.Trim();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schema, authorizationToken);
        }

        public static async Task<T?> Deserialize<T>(this HttpResponseMessage resp, JsonSerializer? serializer = null)
        {
            serializer = serializer ?? JsonSerializer.Create(new JsonSerializerSettings());

            using var stream = await resp.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(reader);
            return serializer.Deserialize<T>(jsonReader);
        }

        public static bool TryGetHeader(this HttpResponseHeaders headers, string header, out string value)
        {
            var found = headers.TryGetValues(header, out var values);
            value = null;
            if (found)
            {
                value = values.First();
            }
            return found;
        }

        public static bool TryGetHeader(this HttpContentHeaders headers, string header, out string value)
        {
            var found = headers.TryGetValues(header, out var values);
            value = null;
            if (found)
            {
                value = values.First();
            }
            return found;
        }

        public static HttpContent CreateJsonContent(object obj)
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings());
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            using var jsonWriter = new JsonTextWriter(writer);
            serializer.Serialize(jsonWriter, obj);
            ByteArrayContent byteArrayContent = new ByteArrayContent(ms.ToArray());
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteArrayContent;
        }
    }
}