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
            if (!student.AttendedLessons.Contains(Lesson))
                throw new LessonNotVisitedException(Lesson, student);

            if (_submittedBy.ContainsKey(student))
                throw new HomeworkAlreadySubmittedException(this);

            _submittedBy.Add(student, submissionDate);
        }

        /// <summary>
        /// Возвращает true, если студент сдал задание позже дня урока.
        /// </summary>
        public bool IsLate(Student student)
        {
            if (!_submittedBy.TryGetValue(student, out var submittedDate))
                return false;

            return submittedDate.Date > Lesson.ClassTime.Date;
        }
    }
}