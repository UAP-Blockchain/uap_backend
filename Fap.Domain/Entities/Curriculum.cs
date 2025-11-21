using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fap.Domain.Entities
{
    /// <summary>
    /// Defines a program structure that students follow across semesters.
    /// </summary>
    public class Curriculum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Code { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string? Description { get; set; }

        public int TotalCredits { get; set; }

        public virtual ICollection<CurriculumSubject> CurriculumSubjects { get; set; } = new List<CurriculumSubject>();

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
