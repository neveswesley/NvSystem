using System.Globalization;

namespace NvSystem.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;  
  
    public CultureMiddleware(RequestDelegate next)  
    {  
        _next = next;  
    }

    public async Task InvokeAsync(HttpContext context)  
    {  
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();  
    
        var cultureInfo = new CultureInfo(requestedCulture);  
  
        CultureInfo.CurrentCulture = cultureInfo;  
        CultureInfo.CurrentUICulture = cultureInfo;
		
        await _next(context);
    }
}