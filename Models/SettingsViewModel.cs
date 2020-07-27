using System.Collections.Generic;
using IdentityServer4.Models;

namespace IronHasura.Models 
{
    public class SettingsViewModel 
    {
        public string HasuraConsoleUrl { get; set; }
        public string FileAdapter { get; set; }
        public string Container { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseHost { get; set; }
        public bool IsTwitterEnable { get; set; }
        public bool IsMicrosoftEnable { get; set; }
        public bool IsGoogleEnable { get; set; }
        public bool IsLinkedInEnable { get; set; }
        public bool IsDropboxEnable { get; set; }
        public bool IsGithubEnable { get; set; }
        public bool IsOIDCEnable { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}