using System.ComponentModel.DataAnnotations;
using static TicketsEchangeSystem.Common.ValidationConstantsForEntities.Status;

namespace TicketsExchangeSystem.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CurrentStatusTitleMaxLength)]
        public string CurrentStatus { get; set; } = null!;
    }
}
