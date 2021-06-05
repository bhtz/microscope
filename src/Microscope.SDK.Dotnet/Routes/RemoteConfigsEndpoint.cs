using System;

namespace Microscope.SDK.Dotnet.Routes
{
    public static class RemoteConfigsEndpoint
    {
        public static string Create = "api/remoteconfig";
        public static string GetAll = "api/remoteconfig";

        public static string Update(Guid id)
        {
            return $"api/remoteconfig/{id.ToString()}";
        }
    }
}