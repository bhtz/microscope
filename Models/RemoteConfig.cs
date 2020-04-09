using System;
using System.ComponentModel.DataAnnotations;

namespace IronHasura.Models
{
    public partial class RemoteConfig
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}