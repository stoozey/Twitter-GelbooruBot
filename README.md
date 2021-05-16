# Gelbooru Twitter Bot

##### This bot connects to the gebooru.com API, finds an image then uploads it to Twitter.

## Nuget Dependencies: TweetinviAPI v4.0.3 and Newtonsoft.Json v12.0.2
## Requires "config.cfg" in working directory in JSON format with values corresponding to those serializable in Tbot/Data/Config.cs
### Here is a template config.cfg to get you started:
```json
{"KEY_CONSUMER":"","KEY_CONSUMER_SECRET":"","KEY_ACCESS":"","KEY_ACCESS_SECRET":"","KEY_GELBOORU_API":"","KEY_GELBOORU_USER_ID":,"PAGE_IMAGE_LIMIT":50,"PAGE_INDEX_MAX":2,"PAGE_SEARCH_TAGS":[""],"POST_INTERVAL_MINS":60}
```
