using System;
using System.ComponentModel.DataAnnotations;

namespace IronHasura.Models
{
    public partial class AppConfig
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}