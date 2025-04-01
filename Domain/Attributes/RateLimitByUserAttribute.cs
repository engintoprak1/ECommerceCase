using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

public class RateLimitAttribute : ActionFilterAttribute
{
    private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private readonly string _rateLimitExceededMessage = "Rate limit exceeded. Please try again later.";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var clientIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();

        if (clientIp == null)
        {
            context.Result = new BadRequestObjectResult("Unable to detect client IP.");
            return;
        }

        if (IsRateLimited(clientIp))
        {
            context.Result = new ObjectResult(_rateLimitExceededMessage)
            {
                StatusCode = 429
            };
            return;
        }

        base.OnActionExecuting(context);
    }

    private bool IsRateLimited(string clientIp)
    {
        // Cache'teki mevcut istek bilgilerini al
        var requestInfo = _cache.Get<RateLimitInfo>(clientIp);

        if (requestInfo == null)
        {
            // İlk istek, hemen başlat
            _cache.Set(clientIp, new RateLimitInfo
            {
                Timestamp = DateTime.Now,
                RequestCount = 1
            });
            return false;
        }

        // Zaman aşımına uğramışsa (geçerli bir saat diliminde değilse)
        if (requestInfo.Timestamp.AddHours(1) < DateTime.Now)
        {
            // Yeni zaman dilimi başlat, sayacı sıfırla
            _cache.Set(clientIp, new RateLimitInfo
            {
                Timestamp = DateTime.Now,
                RequestCount = 1
            });
            return false;
        }

        // Eğer istek sayısı limitin altında ise devam et
        if (requestInfo.RequestCount < 5)
        {
            // İstek sayısını artır
            requestInfo.RequestCount++;
            _cache.Set(clientIp, requestInfo); // Güncelle cache
            return false;
        }

        // Eğer limit aşıldıysa, rate limit'ten dolayı engelle
        return true;
    }
}

public class RateLimitInfo
{
    public DateTime Timestamp { get; set; }
    public int RequestCount { get; set; }
}

