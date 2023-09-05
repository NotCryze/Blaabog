using Microsoft.OpenApi.Attributes;

namespace SBO.BlaaBog.Domain.Entities
{
    public enum ToastColor
    {
        [Display("text-bg-primary")]
        Primary,

        [Display("text-bg-secondary")]
        Secondary,

        [Display("text-bg-success")]
        Success,

        [Display("text-bg-danger")]
        Danger,

        [Display("text-bg-warning")]
        Warning,

        [Display("text-bg-info")]
        Info,

        [Display("text-bg-light")]
        Light,

        [Display("text-bg-dark")]
        Dark
    }
}
