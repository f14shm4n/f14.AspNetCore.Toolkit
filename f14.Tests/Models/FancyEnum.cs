using System.ComponentModel.DataAnnotations;

namespace f14.Tests.Models
{
    public enum FancyEnum
    {
        [Display(Name = "Sample_0")]
        Sample_0 = 0,
        [Display(Name = "Sample_1")]
        Sample_1 = 1
    }
}
