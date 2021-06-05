using System;
using System.Text.Json.Serialization;

namespace Microscope.Hasura.DTO
{
    public class HasuraClaims
    {
        [JsonPropertyName("x-hasura-user-id")]
        public Guid UserId { get; set; }
        
        [JsonPropertyName("x-hasura-role")]
        public string Role { get; set; }
    }
}
