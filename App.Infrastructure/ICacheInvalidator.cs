namespace App.Infrastructure;

public interface ICacheInvalidator<in TRequest>
{
    Task Invalidate(TRequest request);
}