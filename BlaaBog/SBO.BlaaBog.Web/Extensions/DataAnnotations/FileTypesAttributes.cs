using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.Extensions.DataAnnotations
{
    public class FileTypesAttributes : ValidationAttribute
    {
        private readonly string[] _types;

        public FileTypesAttributes(string[] types) : base("{0} must be {1}")
        {
            _types = types;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile file = value as IFormFile;
            string fileType = file.FileName.Split('.').Last();

            return _types.Contains(fileType) ? ValidationResult.Success : new ValidationResult(String.Format("The {0} field only accepts files with the following extensions: {1}.", validationContext.DisplayName, String.Join(", ", _types)));
        }
    }
}
