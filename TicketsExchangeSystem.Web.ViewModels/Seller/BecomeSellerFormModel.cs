namespace TicketsExchangeSystem.Web.ViewModels.Seller
{
    using System.ComponentModel.DataAnnotations;
    using TicketsEchangeSystem.Common;
    public class BecomeSellerFormModel
    {
        [Display(Name = "I accept the above terms of service agreement.")]
        [CheckBoxRequired(ErrorMessage = "Please accept the terms and condition.")]
        public bool Agreed { get; set; }
    }
}
