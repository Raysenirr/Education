using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Models.Base
{
    public interface IModel<out TId> where TId : struct
    {
        public TId Id { get; }

    }
}
