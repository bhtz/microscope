using System;

namespace Microscope.SDK.Dotnet.Routes
{
    public static class AnalyticsEndpoint
    {
        public static string Create = "api/analytic";
        public static string GetAll = "api/analytic";
        public static string Update(Guid id)
        {
            return $"api/analytic/{id.ToString()}";
        }
    }
}