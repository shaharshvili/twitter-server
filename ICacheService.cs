using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterApplication
{
    public interface ICacheService
    {
        public Task<string> GetCacheValueAsync(string key);
        public Task SetCacheValueAsync(string key, string value);
    }
}
