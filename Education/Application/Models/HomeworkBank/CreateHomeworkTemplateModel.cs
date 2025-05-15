using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.HomeworkBank
{
    namespace Education.Application.Models.HomeworkBank
    {
        public class CreateHomeworkTemplateModel
        {
            public required Guid TeacherId { get; init; }
            public required string Topic { get; init; }
            public required string Title { get; init; }
        }
    }
}
