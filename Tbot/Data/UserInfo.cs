using Tweetinvi;
using Tweetinvi.Models;

namespace Tbot.Data
{
    public static class UserInfo
    {
        private static TwitterCredentials Credentials(Config _config)
        {
            var credentials = new TwitterCredentials(
                _config.KEY_CONSUMER, _config.KEY_CONSUMER_SECRET,
                _config.KEY_ACCESS, _config.KEY_ACCESS_SECRET
            );

            return credentials;
        }
        
        public static void Init(Config _config)
            => Auth.SetCredentials( Credentials(_config) );
        
    }
}