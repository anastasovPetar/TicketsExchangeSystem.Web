namespace TicketsEchangeSystem.Common
{
    using System.ComponentModel.DataAnnotations;

    public class CheckBoxRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }

            return false;
        }
    }
}
