namespace TicketsEchangeSystem.Common
{
    using System.ComponentModel.DataAnnotations;
    public class CustomEventDateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime propValue = Convert.ToDateTime(value);

            if (propValue < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }

}
