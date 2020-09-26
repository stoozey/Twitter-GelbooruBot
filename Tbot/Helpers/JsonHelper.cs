using Newtonsoft.Json;

namespace Tbot.Helpers
{
    public static class JsonHelper
    {
        public struct ImageData
        {
            public string file_url;
            public string source;
        }

        public static ImageData[] ConvertJsonToImageData(string _json)
            => DeserializeJson<ImageData[]>(_json);

        private static T DeserializeJson<T>(string _json)
            => JsonConvert.DeserializeObject<T>(_json);
    }
}