using System;
using System.Web.Caching;

namespace Charltone.Extensions
{
    public static class CacheExtensions
    {
        public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator)
        {
            var result = cache[key];

            if (result == null)
            {
                result = generator();
                cache[key] = result;
            }

            return (T)result;
        }

        public static T ReStore<T>(this Cache cache, string key, object obj)
        {
            cache[key] = obj;
            var result = cache[key];

            return (T)result;
        }
    }
}
