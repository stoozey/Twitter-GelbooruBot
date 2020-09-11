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
        {
            var imageData = DeserializeJson<ImageData[]>(_json);
            
            return imageData;
        }

        private static T DeserializeJson<T>(string _json)
        {
            return JsonConvert.DeserializeObject<T>(_json);
        }
    }
}