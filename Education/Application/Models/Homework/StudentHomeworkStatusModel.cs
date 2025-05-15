using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Homework
{
    public class StudentHomeworkStatusModel
    {
        public required Guid StudentId { get; init; }
        public required Guid HomeworkId { get; init; }
        public required bool IsSubmitted { get; init; }
        public DateTime? SubmittedAt { get; init; }
        public bool IsLate => SubmittedAt.HasValue && SubmittedAt.Value > ClassTime;
        public required DateTime ClassTime { get; init; }
    }
}
