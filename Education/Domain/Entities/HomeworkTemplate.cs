using Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Base;

namespace Education.Domain.Entities
{
    public class HomeworkTemplate : Entity<Guid>
    {
        public LessonTopic Topic { get; protected set; }
        public HomeworkTitle Title { get; protected set; }

        protected HomeworkTemplate(Guid id, LessonTopic topic, HomeworkTitle title)
            : base(id)
        {
            Topic = topic;
            Title = title;
        }

        public HomeworkTemplate(LessonTopic topic, HomeworkTitle title)
            : this(Guid.NewGuid(), topic, title)
        {
        }

        protected HomeworkTemplate() : base(Guid.NewGuid()) { }
    }
}
