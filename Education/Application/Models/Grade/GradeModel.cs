using Education.Application.Models.Base;
using Education.Application.Models.Lesson;
using Education.Application.Models.Student;
using Education.Application.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Grade
{
    public class GradeModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required TeacherModel Teacher { get; init; }
        public required StudentModel Student { get; init; }
        public required LessonModel Lesson { get; init; }
        public required DateTime GradedTime { get; init; }
        public required int Mark { get; init; }
    }
}
