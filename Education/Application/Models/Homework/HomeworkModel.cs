using Education.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Homework
{
    public class HomeworkModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required Guid LessonId { get; init; }
        public required string Title { get; init; }
        public required Dictionary<Guid, DateTime> SubmittedBy { get; init; } // StudentId → Date
    }
}
