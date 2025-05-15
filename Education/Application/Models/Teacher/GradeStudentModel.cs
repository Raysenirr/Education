using Education.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Teacher
{
    public class GradeStudentModel
    {
        public required Guid StudentId { get; init; }
        public required Guid TeacherId { get; init; }
        public required Guid LessonId { get; init; }
        public required Mark Mark { get; init; }

    }
}
