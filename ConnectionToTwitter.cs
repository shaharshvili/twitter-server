using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Microsoft.Extensions.Configuration;
using Tweetinvi.Models.DTO;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using TweetSharp;
using System.Text.Json;
using Newtonsoft.Json;

namespace TwitterApplication
{
    public class ConnectionToTwitter
    {
        private ICacheService _cacheService;
        private string apiKey = "ji94kXaBEiPijHevoeVny3TxV";
        private string apiSecretKey = "INB59XdfNXsEwzaemO3ZnfIkkTc0KLYDzaC05zUWkm33asDRLY";
        private string accessToken = "768013783990145024-JOfWCd4R8EMac4nJ3T5xI4DcQi2t6Up";
        private string accessTokenSecret = "ZlmR5uUoaPTKLWU8EqFY5WPSMU2KooJJyk8xcDB7Qaufd";
        private readonly IConfiguration Configuration;
        public ConnectionToTwitter(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<List<TwitterStatus>> GetTweetsFromHashtagAsync(string hashtag)
        {
            var results = await _cacheService.GetCacheValueAsync(hashtag);
            if (results != null)
            {
                var resultsFromCache = JsonConvert.DeserializeObject<List<TwitterStatus>>(results);
                return resultsFromCache; 
            }
            else
            {
                TwitterService twitterService = new TwitterService(apiKey, apiSecretKey);
                twitterService.AuthenticateWith(accessToken, accessTokenSecret);
                var tweets_search = twitterService.Search(new SearchOptions { Q = "#" + hashtag, Resulttype = TwitterSearchResultType.Recent, Count = 10 });
                var listResults = new List<TwitterStatus>(tweets_search.Statuses);
                var serializedResults = JsonConvert.SerializeObject(listResults); 
                await _cacheService.SetCacheValueAsync(hashtag,serializedResults);
                return listResults;
            }
        }

        public List<TwitterStatus> GetTweetsByLocation(Location location)
        {
            TwitterService twitterService = new TwitterService(apiKey, apiSecretKey);
            twitterService.AuthenticateWith(accessToken, accessTokenSecret);
            TwitterGeoLocationSearch twitterGeoLocationSearch = new TwitterGeoLocationSearch(latitutde: location._lat, longitude: location._long, radius: location._radius,TwitterGeoLocationSearch.RadiusType.Km);
            var tweets_search_result = twitterService.Search(new SearchOptions{ Geocode = twitterGeoLocationSearch,Count=10});
            return new List<TwitterStatus>(tweets_search_result.Statuses);
        }
    }
}
