using Education.Domain.Exceptions;
using Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Хранилище шаблонов домашних заданий, привязанных к темам.
    /// </summary>
    public class HomeworkBank
    {
        private readonly ICollection<HomeworkTemplate> _templates = new List<HomeworkTemplate>();

        /// <summary>
        /// Только для чтения — список шаблонов.
        /// </summary>
        public IReadOnlyCollection<HomeworkTemplate> Templates => _templates.ToList().AsReadOnly();

        /// <summary>
        /// Добавляет новое шаблонное задание.
        /// </summary>
        public void AddTemplate(LessonTopic topic, HomeworkTitle title)
        {
            if (_templates.Any(t => t.Topic.Value == topic.Value))
                throw new DuplicateHomeworkTemplateException(topic);

            _templates.Add(new HomeworkTemplate(topic, title));
        }

        /// <summary>
        /// Получает существующее задание по теме, если оно есть.
        /// </summary>
        public HomeworkTemplate? GetTemplateByTopic(LessonTopic topic)
        {
            return _templates.FirstOrDefault(t => t.Topic.Value == topic.Value);
        }
    }
}