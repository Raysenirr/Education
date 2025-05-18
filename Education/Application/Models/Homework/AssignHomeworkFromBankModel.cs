using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Homework
{

    public class AssignHomeworkFromBankModel
    {

        public required Guid TeacherId { get; init; }
        public required Guid LessonId { get; init; }
    }
}
