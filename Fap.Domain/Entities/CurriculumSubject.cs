using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fap.Domain.Entities
{
    /// <summary>
    /// Links subjects to a curriculum with semester and prerequisite metadata.
    /// </summary>
    public class CurriculumSubject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CurriculumId { get; set; }

        [Required]
        public Guid SubjectId { get; set; }

        /// <summary>
        /// Recommended semester order (1-based); supports flexible study plans.
        /// </summary>
        [Range(1, 20)]
        public int SemesterNumber { get; set; }

        /// <summary>
        /// Optional prerequisite subject within the same curriculum.
        /// </summary>
        public Guid? PrerequisiteSubjectId { get; set; }

        [ForeignKey(nameof(CurriculumId))]
        public virtual Curriculum Curriculum { get; set; } = null!;

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; } = null!;

        [ForeignKey(nameof(PrerequisiteSubjectId))]
        public virtual Subject? PrerequisiteSubject { get; set; }
    }
}
