using Education.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Student
{
    public class CreateStudentModel : PersonCreateModel
    {
        public required Guid GroupId { get; init; }

    }
}
