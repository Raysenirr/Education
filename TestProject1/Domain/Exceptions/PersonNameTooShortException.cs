using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Exceptions
{
    public class PersonNameTooShortException(string value, int minLength)
        : ArgumentOutOfRangeException(nameof(value), value, $"Group name must be at least {minLength} characters long.")
    { }
}
