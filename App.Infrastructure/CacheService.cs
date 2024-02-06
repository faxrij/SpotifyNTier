using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace App.Infrastructure;

public class CacheService : ICache
{
	private readonly string? _keyPrefix;
	private readonly IDistributedCache _cache;

	public CacheService(IDistributedCache cache, IConfiguration config)
	{
		_cache = cache;
		var keyPrefix = config["CacheKeyPrefix"];
		_keyPrefix = keyPrefix ?? typeof(CacheService).FullName;
	}

	public string GetCacheKey(string key)
	{
		return MakeKey(key);
	}

	public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
	{
		return await GetInternalAsync<T>(key, token);
	}

	public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, CancellationToken token = default)
	{
		return await GetOrCreateInternalAsync<T>(key, null, null, null, factory, token);
	}

	public async Task<T> GetOrCreateAsync<T>(string key, TimeSpan slidingExpiration, Func<Task<T>> factory, CancellationToken token = default)
	{
		return await GetOrCreateInternalAsync<T>(key, slidingExpiration, null, null, factory, token);
	}

	public async Task<T> GetOrCreateAsync<T>(string key, DateTime absoluteExpiration, Func<Task<T>> factory, CancellationToken token = default)
	{
		return await GetOrCreateInternalAsync<T>(key, null, absoluteExpiration, null, factory, token);
	}

	public async Task<T> GetOrCreateAsync<T>(string key, TimeSpan slidingExpiration, DateTime absoluteExpiration, Func<Task<T>> factory, CancellationToken token = default)
	{
		return await GetOrCreateInternalAsync<T>(key, slidingExpiration, absoluteExpiration, null, factory, token);
	}

	public async Task<T> GetOrCreateAsync<T>(string key, TimeSpan? slidingExpiration, DateTime? absoluteExpiration, TimeSpan? absoluteExpirationRelativeToNow, Func<Task<T>> factory, CancellationToken token = default)
	{
		return await GetOrCreateInternalAsync(key, slidingExpiration, absoluteExpiration, absoluteExpirationRelativeToNow, factory, token);
	}

	public async Task SetAsync<T>(string key, T value, CancellationToken token = default)
	{
		await SetInternalAsync(key, value, null, null, null, token);
	}

	public async Task SetAsync<T>(string key, T value, DateTime absoluteExpiration,
		CancellationToken token = default)
	{
		await SetInternalAsync(key, value, null, absoluteExpiration, null, token);
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration,
		CancellationToken token = default)
	{
		await SetInternalAsync(key, value, slidingExpiration, null, null, token);
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration, DateTime? absoluteExpiration,
		CancellationToken token = default)
	{
		await SetInternalAsync(key, value, slidingExpiration, absoluteExpiration, null, token);
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration, TimeSpan? absoluteExpirationRelativeToNow,
		CancellationToken token = default)
	{
		await SetInternalAsync(key, value, slidingExpiration, null, absoluteExpirationRelativeToNow, token);
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration, DateTime? absoluteExpiration,
		TimeSpan? absoluteExpirationRelativeToNow, CancellationToken token = default)
	{
		await SetInternalAsync(key, value, slidingExpiration, absoluteExpiration, absoluteExpirationRelativeToNow, token);
	}

	public async Task RefreshAsync(string key, CancellationToken token = default)
	{
		await _cache.RefreshAsync(key, token);
	}

	public async Task RemoveAsync(string key, CancellationToken token = default)
	{
		await _cache.RemoveAsync(key, token);
	}

	private string MakeKey(string key)
	{
		return $"{(string.IsNullOrWhiteSpace(_keyPrefix) ? "" : _keyPrefix + ":")}{key}";
	}

	private async Task<T> GetInternalAsync<T>(string key, CancellationToken token = default)
	{
		byte[]? cachedBytes = await _cache.GetAsync(MakeKey(key), token);
		
		if (cachedBytes == null)
		{
			return default!;
		}
		
		string serializedValue = Encoding.UTF8.GetString(cachedBytes);
		T deserializedValue = JsonConvert.DeserializeObject<T>(serializedValue);
		return deserializedValue;
	}


	private async Task<T> GetOrCreateInternalAsync<T>(string key, TimeSpan? slidingExpiration, DateTime? absoluteExpiration, TimeSpan? absoluteExpirationRelativeToNow, Func<Task<T>> factory, CancellationToken token = default)
	{
		var value = await GetInternalAsync<T>(key, token);

		if (value != null) return value;

		value = await factory();

		if (value != null)
		{
			await SetInternalAsync<T>(key, value, slidingExpiration, absoluteExpiration, absoluteExpirationRelativeToNow, token);
		}

		return value;
	}

	private async Task SetInternalAsync<T>(string key, T value, TimeSpan? slidingExpiration, DateTime? absoluteExpiration, TimeSpan? absoluteExpirationRelativeToNow, CancellationToken token = default)
	{
		var cacheEntryOptions = new DistributedCacheEntryOptions();
		if (slidingExpiration.HasValue)
		{
			cacheEntryOptions.SetSlidingExpiration(slidingExpiration.Value);
		}

		if (absoluteExpiration.HasValue)
		{
			cacheEntryOptions.SetAbsoluteExpiration(absoluteExpiration.Value);
		}

		if (absoluteExpirationRelativeToNow.HasValue)
		{
			cacheEntryOptions.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow.Value;
		}

		if (!slidingExpiration.HasValue && !absoluteExpiration.HasValue && !absoluteExpirationRelativeToNow.HasValue)
		{
			cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(30));
			cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
		}

		// Serialize the value object to JSON
		string serializedValue = JsonConvert.SerializeObject(value);
		byte[] serializedBytes = Encoding.UTF8.GetBytes(serializedValue);

		// Store the serialized bytes in the distributed cache
		await _cache.SetAsync(MakeKey(key), serializedBytes, cacheEntryOptions, token);
	}

}