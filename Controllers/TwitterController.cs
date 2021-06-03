using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using TweetSharp;

namespace TwitterApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        ConnectionToTwitter connectionToTwitter;
        private readonly ICacheService _cacheService;
        public TwitterController(ICacheService cacheService)
        {
            _cacheService = cacheService;
            connectionToTwitter = new ConnectionToTwitter(_cacheService);
        }
        [HttpGet("{hashtag}")]
        public async Task<List<TwitterStatus>> GetTwittersByHashtag(string hashtag)
        {
            var result = await connectionToTwitter.GetTweetsFromHashtagAsync(hashtag);
            return result; 
        }

        [HttpGet("getByLocation/{la}/{lo}/{r}")]
        public List<TwitterStatus> GetTweetsByLocation(double la,double lo , int r)
        {
            Location location = new Location(la, lo,r);
            var h = connectionToTwitter.GetTweetsByLocation(location);
            return h; 
        }
    }
}