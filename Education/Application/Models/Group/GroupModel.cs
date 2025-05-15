using Education.Application.Models.Base;
using Education.Application.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Group
{
    public class GroupModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required IEnumerable<StudentModel> Students { get; init; }
    }
}
