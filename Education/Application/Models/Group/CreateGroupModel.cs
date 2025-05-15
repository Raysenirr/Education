using Education.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Group
{
    public class CreateGroupModel : ICreateModel
    {
        public required string Name { get; init; }

    }
}
