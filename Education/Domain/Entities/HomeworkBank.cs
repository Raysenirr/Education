using Education.Domain.Exceptions;
using Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Хранилище шаблонов домашних заданий, привязанных к темам уроков.
    /// Используется как owned entity внутри Teacher.
    /// </summary>
    public class HomeworkBank
    {
        private readonly ICollection<HomeworkTemplate> _templates = new List<HomeworkTemplate>();

        /// <summary>
        /// Только для EF
        /// </summary>
        private HomeworkBank() { }


        public HomeworkBank(bool createEmpty = true)
        {
        }

        /// <summary>
        /// Конструктор для восстановления из БД 
        /// </summary>
        protected HomeworkBank(ICollection<HomeworkTemplate> templates)
        {
            _templates = templates ?? throw new TemplatesIsNullException();
        }

        /// <summary>
        /// Получить все шаблоны в виде коллекции
        /// </summary>
        public IReadOnlyCollection<HomeworkTemplate> Templates =>
            new ReadOnlyCollection<HomeworkTemplate>(_templates.ToList());

        /// <summary>
        /// Добавить новый шаблон задания
        /// </summary>
        public void AddTemplate(LessonTopic topic, HomeworkTitle title)
        {
            if (topic == null)
                throw new LessonTopicIsNullException();

            if (title == null)
                throw new HomeworkTitleIsNullException();

            if (_templates.Any(t => t.Topic.Equals(topic)))
                throw new DuplicateHomeworkTemplateException(topic);

            _templates.Add(new HomeworkTemplate(topic, title));
        }

        /// <summary>
        /// Найти шаблон по теме
        /// </summary>
        public HomeworkTemplate? FindTemplate(LessonTopic topic)
        {
            return _templates.FirstOrDefault(t => t.Topic.Equals(topic));
        }

        /// <summary>
        /// Удалить шаблон по теме
        /// </summary>
        public bool RemoveTemplate(LessonTopic topic)
        {
            var template = _templates.FirstOrDefault(t => t.Topic.Equals(topic));
            return template != null && _templates.Remove(template);
        }
    }
}

