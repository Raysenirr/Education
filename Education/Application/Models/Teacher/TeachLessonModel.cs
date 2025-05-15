using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Teacher
{
    public class TeachLessonModel
    {
        public required Guid TeacherId { get; init; }
        public required Guid LessonId { get; init; }
    }
}
