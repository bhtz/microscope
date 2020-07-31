using System;
using System.ComponentModel.DataAnnotations;

namespace Microscope.Data
{
    public partial class RemoteConfig
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}