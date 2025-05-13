using Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class HomeworkTemplate
    {
        public LessonTopic Topic { get; }
        public HomeworkTitle Title { get; }

        public HomeworkTemplate(LessonTopic topic, HomeworkTitle title)
        {
            Topic = topic;
            Title = title;
        }
    }
}
