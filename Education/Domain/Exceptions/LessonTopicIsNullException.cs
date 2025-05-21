using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Exceptions
{
    internal class LessonTopicIsNullException()
        : ArgumentNullException("Topic", "Lesson cannot be null.")
    {
        public string Field => "Topic";
    }
}
