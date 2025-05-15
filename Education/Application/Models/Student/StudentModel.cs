using Education.Application.Models.Base;
using Education.Application.Models.Teacher;
using Education.Application.Models.Student;
using Education.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Application.Models.Grade;
using Education.Application.Models.Lesson;
using Education.Application.Models.Group;

namespace Education.Application.Models.Student
{
    public class StudentModel : PersonModel
    {
        public IEnumerable<GradeModel> RecievedGrades { get; init; } = [];
        public IEnumerable<LessonModel> AttendedLessons { get; init; } = [];
        public required GroupModel Group { get; init; }
    }
}
