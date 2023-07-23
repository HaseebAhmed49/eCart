using System;
namespace eCart.API.Data.Services.Caching
{
	public interface IResponseCacheService
	{
		Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

		Task<string> GetCachedResponseAsync(string cacheKey);
	}
}

