using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Homework
{
    public class AssignedHomeworkModel
    {
        public required Guid HomeworkId { get; init; }
        public required string Title { get; init; }
        public required Guid LessonId { get; init; }
        public required DateTime ClassTime { get; init; }
    }
}
