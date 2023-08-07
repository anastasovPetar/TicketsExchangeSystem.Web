

namespace TicketsEchangeSystem.Common
{
    using System.ComponentModel.DataAnnotations;
    public class CustomEventDateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //DateTime dtFromForm = Convert.ToDateTime(value);
           
            //if (DateTime.Now >= dtFromForm)
            //{
            //    return false;
            //}

            //return true;
            if ((DateTime?) value < DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
