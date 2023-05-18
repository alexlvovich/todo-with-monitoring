using Konso.Clients.ValueTracking.Interfaces;
using Konso.Clients.ValueTracking.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Net.Http.Headers;

namespace todo.Models
{
    public class KonsoModel : PageModel
    {
        internal readonly IValueTrackingClient _valueTrackingService;

        public KonsoModel(IValueTrackingClient valueTrackingService)
        {
            _valueTrackingService = valueTrackingService;
        }

        internal async Task TrackValue(string user, ValueTrackingTypes type, double? value = null)
        {
            await _valueTrackingService.CreateAsync(new ValueTrackingCreateRequest()
            {
                IP = Request != null ? Request.HttpContext.Connection.RemoteIpAddress.ToString() : string.Empty,
                EventId = (int)type,
                User = user,
                Value = value,
                CorrelationId = Request.HttpContext.Session.Id,
                Browser = Request.Headers[HeaderNames.UserAgent],
                AppName = "todoapp",
                Tags = new List<string>() { Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") }
            });
        }
    }
}
