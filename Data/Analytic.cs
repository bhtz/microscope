using System;
using System.ComponentModel.DataAnnotations;

namespace IronHasura.Data
{
    public partial class Analytic
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Dimension { get; set; }
    }
}