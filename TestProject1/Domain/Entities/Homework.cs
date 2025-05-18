using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Base;
using Education.Domain.ValueObjects;
using Education.Domain.Exceptions;
using Education.Domain.Enums;
using Education.Domain.ValueObjects.Validators;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Домашнее задание, выданное по теме урока.
    /// </summary>
    public class Homework : Entity<Guid>
    {
        public Lesson Lesson { get; }
        public HomeworkTitle Title { get; }

        // Сдачи: кто и когда сдал
        private readonly Dictionary<Student, DateTime> _submittedBy = new();
        public IReadOnlyDictionary<Student, DateTime> SubmittedBy => _submittedBy;

        protected Homework(Guid id, Lesson lesson, HomeworkTitle title) : base(id)
        {
            Lesson = lesson;
            Title = title;
        }
        public Homework(Lesson lesson, HomeworkTitle title) : this(Guid.NewGuid(), lesson, title)
        {
        }
        protected Homework() : base(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Сдать домашнее задание от студента.
        /// </summary>
        public void SubmitBy(Student student, DateTime submissionDate)
        {
            // 1. Проверка посещения урока
            if (!student.AttendedLessons.Contains(Lesson))
                throw new LessonNotVisitedException(Lesson, student);

            // 2. Проверка повторной сдачи
            if (_submittedBy.ContainsKey(student))
                throw new HomeworkAlreadySubmittedException(this);

            // 3. Проверка что дата сдачи не в будущем
            if (submissionDate > DateTime.UtcNow.AddMinutes(1)) // +1 минута как буфер
                throw new InvalidSubmissionDateException(submissionDate);

            // 4. Фиксация сдачи
            _submittedBy[student] = submissionDate; // Используем индексатор для обновления если ключ существует
        }



        /// <summary>
        /// Возвращает true, если студент сдал задание позже дня урока.
        /// </summary>
        public bool IsLate(Student student)
        {
            if (!_submittedBy.TryGetValue(student, out var submittedDate))
                return false;

            // Добавляем буфер в 1 секунду для надёжности
            return submittedDate - Lesson.ClassTime > TimeSpan.FromSeconds(1);
        }

    }
}