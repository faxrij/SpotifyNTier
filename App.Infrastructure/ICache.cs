namespace App.Infrastructure;

public interface ICache
	{
		string GetCacheKey(string key);

		Task<T> GetAsync<T>(string key, CancellationToken token = default);

		Task<T> GetOrCreateAsync<T>(
			string key,
			Func<Task<T>> factory,
			CancellationToken token = default);

		Task<T> GetOrCreateAsync<T>(
			string key,
			TimeSpan slidingExpiration,
			Func<Task<T>> factory,
			CancellationToken token = default);

		Task<T> GetOrCreateAsync<T>(
			string key,
			DateTime absoluteExpiration,
			Func<Task<T>> factory,
			CancellationToken token = default);

		Task<T> GetOrCreateAsync<T>(
			string key,
			TimeSpan slidingExpiration,
			DateTime absoluteExpiration,
			Func<Task<T>> factory,
			CancellationToken token = default);

		Task<T> GetOrCreateAsync<T>(
			string key,
			TimeSpan? slidingExpiration,
			DateTime? absoluteExpiration,
			TimeSpan? absoluteExpirationRelativeToNow,
			Func<Task<T>> factory,
			CancellationToken token = default);

		Task SetAsync<T>(
			string key,
			T value,
			CancellationToken token = default);

		Task SetAsync<T>(
			string key,
			T value,
			DateTime absoluteExpiration,
			CancellationToken token = default);

		Task SetAsync<T>(
			string key,
			T value,
			TimeSpan slidingExpiration,
			CancellationToken token = default);

		Task SetAsync<T>(
			string key,
			T value,
			TimeSpan? slidingExpiration,
			DateTime? absoluteExpiration,
			CancellationToken token = default);

		Task SetAsync<T>(
			string key,
			T value,
			TimeSpan? slidingExpiration,
			TimeSpan? absoluteExpirationRelativeToNow,
			CancellationToken token = default);

		Task SetAsync<T>(
			string key,
			T value,
			TimeSpan? slidingExpiration,
			DateTime? absoluteExpiration,
			TimeSpan? absoluteExpirationRelativeToNow,
			CancellationToken token = default);

		Task RefreshAsync(string key, CancellationToken token = default);

		Task RemoveAsync(string key, CancellationToken token = default);
	}