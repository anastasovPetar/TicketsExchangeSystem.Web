namespace TicketsEchangeSystem.Common
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    public class EventDateValidate : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            
            if (value == null)
            {
                return false;
            }

            // Event must start in the future time.
            DateTime dateEvent = (DateTime)value!;
            return (dateEvent > DateTime.Now);
        }        
    }
}
