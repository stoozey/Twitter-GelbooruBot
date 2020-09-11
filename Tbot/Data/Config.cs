using System;
using System.IO;
using System.Numerics;
using Newtonsoft.Json;

namespace Tbot.Data
{
    [Serializable]
    public class Config
    {
        public static string DIR_CONFIG_FILE = $@"{Directory.GetCurrentDirectory()}\config.cfg";
        
        public static string FILE_IMAGE
            => $@"{Directory.GetCurrentDirectory()}\image";
        
        
        public bool COMPUTE_ONCE = false;

        public string KEY_CONSUMER        = "";
        public string KEY_CONSUMER_SECRET = "";
        
        public string KEY_ACCESS        = "";
        public string KEY_ACCESS_SECRET = "";

        public string KEY_GELBOORU_API  = "";
        public int KEY_GELBOORU_USER_ID = -1;
        
        public int PAGE_IMAGE_LIMIT      = -1;
        public int PAGE_INDEX_MAX        = -1;
        public string[] PAGE_SEARCH_TAGS = { "" };
        
        [JsonIgnore] public string GelbooruApiString
            => $"&api_key={KEY_GELBOORU_API}&user_id={KEY_GELBOORU_USER_ID}";
        
        [JsonIgnore] public TimeSpan TWEET_INTERVAL
            => new TimeSpan(0, POST_INTERVAL_MINS, 0);

        public int POST_INTERVAL_MINS = 1;
        
        public void SaveConfig()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(DIR_CONFIG_FILE, json);
        }
        
        public Config()
        {
            if (!File.Exists(DIR_CONFIG_FILE))
            {
                Console.WriteLine("ERROR: Config file was not found.");
                return;
            }
            
            var json = File.ReadAllText(DIR_CONFIG_FILE);
            JsonConvert.PopulateObject(json, this);
            
            Console.WriteLine($"Found config file with settings: {json}");
            Console.WriteLine("####################################################");
        }
    }
}