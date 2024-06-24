namespace Umbraco.Commerce.DemoStore.Import.Core.Models.Validation
{
    public class ValidationResult
    {
        private bool _isValid;

        public bool IsValid
        {
            get
            {
                if (_isValid)
                {
                    return !ErrorMessages.Any();
                }
                return false;
            }
            set
            {
                _isValid = value;
            }
        }

        public List<string> ErrorMessages { get; private set; }

        public ValidationResult()
        {
            ErrorMessages = new List<string>();
            IsValid = true;
        }
    }

}
