using Education.Application.Models.Base;
using Education.Application.Models.Teacher;
using Education.Application.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Lesson
{
    public class LessonModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required TeacherModel Teacher { get; init; }
        public required GroupModel Group { get; init; }
        public required string Topic { get; init; }
        public required DateTime ClassTime { get; init; }
    }
}
