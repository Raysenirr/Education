﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Base
{
    public abstract class PersonCreateModel : ICreateModel
    {
        public required string Name { get; init; }
    }
}
