using Education.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.HomeworkBank
{
    public class HomeworkTemplateModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required string Topic { get; init; }
        public required string Title { get; init; }
    }
}
