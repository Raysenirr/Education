using Education.Application.Models.Base;
using Education.Application.Models.Grade;
using Education.Application.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Teacher
{
    public class TeacherModel : PersonModel
    {
        public required IEnumerable<LessonModel> TeachedLessons { get; init; }
        public required IEnumerable<LessonModel> SchedulledLessons { get; init; }
        public required IEnumerable<GradeModel> AssignedGrades { get; init; }

    }
}
