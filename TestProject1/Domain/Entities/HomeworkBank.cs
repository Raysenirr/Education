using Education.Domain.Exceptions;
using Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Хранилище шаблонов домашних заданий, привязанных к темам.
    /// </summary>

        public class HomeworkBank
        {
            private ICollection<HomeworkTemplate> _templates;

            // Основной защищённый конструктор (для EF и наследования)
            protected HomeworkBank(Guid id, ICollection<HomeworkTemplate> templates)
            {
                Id = id;
                _templates = templates ?? new List<HomeworkTemplate>();
            }

            // Публичный конструктор для создания нового банка
            public HomeworkBank(ICollection<HomeworkTemplate> templates)
                : this(Guid.NewGuid(), templates)
            {
            }

            // Альтернативный публичный конструктор (удобный API)
            public HomeworkBank(params HomeworkTemplate[] templates)
                : this(new List<HomeworkTemplate>(templates))
            {
            }

            // Защищённый конструктор для EF (минимальная инициализация)
            protected HomeworkBank() : this(Guid.Empty, new List<HomeworkTemplate>())
            {
            }

            public Guid Id { get; protected set; }

            public IReadOnlyCollection<HomeworkTemplate> Templates =>
                _templates.ToList().AsReadOnly(); // Создаём новый список для безопасности

            public void AddTemplate(LessonTopic topic, HomeworkTitle title)
            {
                if (_templates.Any(t => t.Topic.Value == topic.Value))
                    throw new DuplicateHomeworkTemplateException(topic);

                _templates.Add(new HomeworkTemplate(topic, title));
            }

            public HomeworkTemplate? FindTemplate(LessonTopic topic)
            {
                return _templates.FirstOrDefault(t => t.Topic.Value == topic.Value);
            }

            public bool RemoveTemplate(LessonTopic topic)
            {
                var template = _templates.FirstOrDefault(t => t.Topic.Value == topic.Value);
                return template != null && _templates.Remove(template);
            }
        }
    }