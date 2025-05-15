using Education.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Lesson
{
    public class CreateLessonModel : ICreateModel
    {
        public required string Topic { get; init; }
        public required Guid GroupId { get; init; }
        public required DateTime ClassTime { get; init; }
        public required Guid TeacherId { get; init; }

    }
}
