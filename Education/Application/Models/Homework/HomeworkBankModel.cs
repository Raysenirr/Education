using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.HomeworkBank
{
    public class HomeworkBankModel
    {
        public required Guid TeacherId { get; init; }
        public required IEnumerable<HomeworkTemplateModel> Templates { get; init; }
    }
}
