using System;

namespace Microscope.SDK.Dotnet.Routes
{
    public static class StoragesEndpoint
    {
        public static string CreateContainer = "api/storage";
        public static string GetAllContainer = "api/storage";

        public static string GetAllBlobsInContainer(string container)
        {
            return $"api/storage/{container}";
        }

        public static string PostBlobInContainer(string container)
        {
            return $"api/storage/{container}";
        }

        public static string GetBlobInContainer(string container, string blob)
        {
            return $"api/storage/{container}/{blob}";
        }

        public static string DeleteBlobInContainer(string container, string blob)
        {
            return $"api/storage/{container}/{blob}";
        }
    }
}