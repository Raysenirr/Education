using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Student
{
    public class VisitLessonModel
    {
        public required Guid StudentId { get; init; }
        public required Guid LessonId { get; init; }

    }
}
