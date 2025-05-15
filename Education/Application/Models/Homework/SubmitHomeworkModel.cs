using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Homework
{
    public class SubmitHomeworkModel
    {
        public required Guid StudentId { get; init; }
        public required Guid HomeworkId { get; init; }
        public required DateTime SubmittedAt { get; init; }
    }
}
