using Education.Domain.ValueObjects;
using Education.Domain.Entities.Base;
using Education.Domain.Exceptions;
using System;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Шаблон домашнего задания (Value Object, Owned by HomeworkBank)
    /// </summary>
    public class HomeworkTemplate : Entity<Guid>
    {
        public LessonTopic Topic { get; private set; }
        public HomeworkTitle Title { get; private set; }

        /// <summary>
        /// Конструктор для доменной логики.
        /// </summary>
        public HomeworkTemplate(LessonTopic topic, HomeworkTitle title)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        /// <summary>
        /// EF Core требует конструктор без параметров.
        /// </summary>
        protected HomeworkTemplate()
        {
            Topic = null!;
            Title = null!;
        }

        /// <summary>
        /// Обновление темы (опционально, если шаблон может изменяться).
        /// </summary>
        public void UpdateTopic(LessonTopic newTopic)
        {
            Topic = newTopic ?? throw new ArgumentNullException(nameof(newTopic));
        }

        /// <summary>
        /// Обновление заголовка (опционально, если шаблон может изменяться).
        /// </summary>
        public void UpdateTitle(HomeworkTitle newTitle)
        {
            Title = newTitle ?? throw new ArgumentNullException(nameof(newTitle));
        }
    }
}
