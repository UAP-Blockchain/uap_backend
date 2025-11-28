using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fap.Domain.DTOs.Slot
{
    /// <summary>
    /// Lightweight slot definition used when managing slots within the scope of a class.
    /// </summary>
    public class CreateClassSlotRequest
    {
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public Guid? TimeSlotId { get; set; }

        public Guid? SubstituteTeacherId { get; set; }

        [MaxLength(500, ErrorMessage = "Substitution reason cannot exceed 500 characters")]
        public string? SubstitutionReason { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Wrapper request for generating multiple slots for a class in a single call.
    /// </summary>
    public class BulkCreateClassSlotsRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one slot definition is required")]
        public List<CreateClassSlotRequest> Slots { get; set; } = new();
    }
}
