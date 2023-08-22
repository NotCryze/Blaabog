using Microsoft.OpenApi.Attributes;

namespace SBO.BlaaBog.Domain.Entities
{
    public enum Specialities : byte
    {
        [Display("None")]
        None = 0,

        [Display("IT Supporter")]
        ITSupporter = 1,

        [Display("Programmer")]
        Programmer = 2,

        [Display("Infrastructure")]
        Infrastructure = 3
    }
}
