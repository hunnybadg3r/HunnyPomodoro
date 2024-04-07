namespace HunnyPomodoro.Validation
{
    public class StringLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public StringLengthRule(int minLength, int maxLength)
        {
            if (minLength < 0)
                throw new ArgumentException("Minimum length cannot be negative.", nameof(minLength));

            if (maxLength < minLength)
                throw new ArgumentException("Maximum length cannot be less than minimum length.", nameof(maxLength));

            MinLength = minLength;
            MaxLength = maxLength;
        }

        public bool Check(T value)
        {
            if (value is string str)
            {
                int length = str.Length;
                return length >= MinLength && length <= MaxLength;
            }

            return false;
        }
    }
}
