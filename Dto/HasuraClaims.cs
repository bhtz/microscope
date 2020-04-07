using System;
using Newtonsoft.Json;

namespace IronHasura.Dto
{
    [JsonObject]
    public class HasuraClaims
    {
        [JsonProperty("x-hasura-user-id")]
        public Guid UserId { get; set; }
        
        [JsonProperty("x-hasura-role")]
        public string Role { get; set; }
    }
}