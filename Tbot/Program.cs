using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

using Tweetinvi;
using Tweetinvi.Models;

using Tbot.Data;
using Tbot.Helpers;
using Tweetinvi.Parameters;

namespace Tbot
{
    internal class Program
    {
        private static bool sendingTweets = true;
        
        private static WebClient webClient;

        private static Config config;
        
        private static void Main(string[] _args)
        {
            InitBotData();
            
            UserInfo.Init(config);

            TweetLoop();
        }

        private static void InitBotData()
        {
            webClient = new WebClient();
            config    = new Config();
        }
        
        #region Tweeting methods
        
        private static void TweetLoop()
        {
            Console.WriteLine("~~~Initiated Bot Loop~~~");

            while (sendingTweets)
            {
                TweetImage();

                if (config.COMPUTE_ONCE)
                {
                    sendingTweets = false;
                    return;
                }
                
                Thread.Sleep(config.TWEET_INTERVAL);
            }

            Console.WriteLine("~~~Terminated Bot Loop~~~");
        }
        
        private static void TweetImage()
        {
            Console.WriteLine("~Initiating new tweet~");

            var imageData = GetImage();
            if (imageData.file_url == null)
            {
                Console.WriteLine("~Tweet failed~");
                return;
            }
            var imageBytes = DownloadImage( imageData.file_url );

            var imageSource = (string.IsNullOrEmpty(imageData.source) ? "Not provided." : imageData.source);
            var tweetText = $"Source: {imageSource}";
            var tweetMedia = Upload.UploadBinary(imageBytes);
            
            Console.WriteLine("Uploading tweet...");
            var tweetData = Tweet.PublishTweet(tweetText, new PublishTweetOptionalParameters()
            {
                Medias = new List<IMedia>() { tweetMedia }
            });
            
            Console.WriteLine("~Tweet sent~");
        }
        
        private static byte[] DownloadImage(string _imageUrl)
        {
            Console.WriteLine("Downloading the image...");
            
            using (webClient)
            {
                webClient.UseDefaultCredentials = true;
                webClient.DownloadFile(new Uri(_imageUrl), Config.FILE_IMAGE);
            }
            
            Console.WriteLine("Downloaded");
            
            return File.ReadAllBytes(Config.FILE_IMAGE);
        }
        
        private static JsonHelper.ImageData GetImage()
        {
            Console.WriteLine("Finding a new image...");
            
            var random = new Random();
            var pageIndex = config.PAGE_INDEX_MAX;

            var availableTags = config.PAGE_SEARCH_TAGS;
            var searchTag = availableTags[ random.Next(0, availableTags.Length - 1) ];
            var address =
                $"https://gelbooru.com/index.php?page=dapi&s=post&q=index&tags={searchTag}&pid={pageIndex}&limit={config.PAGE_IMAGE_LIMIT}&json=1{config.GelbooruApiString}";
            var json = webClient.DownloadString(address);

            Console.WriteLine($"Searching for {address}");
            Console.WriteLine($"Found for {json}");

            var imageData = JsonHelper.ConvertJsonToImageData(json);
            if (imageData.Length <= 0) return new JsonHelper.ImageData();

            return imageData[ random.Next(0, imageData.Length - 1) ];
        }
        
        #endregion
        
    }
}