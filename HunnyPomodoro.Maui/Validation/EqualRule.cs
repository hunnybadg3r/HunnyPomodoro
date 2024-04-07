namespace HunnyPomodoro.Validation
{
    public class EqualRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public ValidatableObject<string> ExpectedValue { get; set; }

        public EqualRule(object expectedValue)
        {
            if (expectedValue is ValidatableObject<string> validatableObject)
            {
                ExpectedValue = validatableObject;
            }
        }

        public bool Check(T value)
        {
            if (ExpectedValue is not null && value is string && value.Equals(ExpectedValue.Value))
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }
    }
}
