using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Base;
using Education.Domain.ValueObjects;
using Education.Domain.Enums;
using Education.Domain.Exceptions;
using System.Text.RegularExpressions;
using Education.Domain.ValueObjects.Validators;

namespace Education.Domain.Entities
{
    public class Lesson : Entity<Guid>
    {
        #region Public Readonly Properties
        public Group Group { get; }
        public Teacher Teacher { get; }
        public DateTime ClassTime { get; private set; }
        public LessonTopic Topic { get; }
        public LessonStatus State { get; private set; }
        #endregion
        private readonly ICollection<Homework> _homeworks = new List<Homework>();
        public IReadOnlyCollection<Homework> AssignedHomeworks => _homeworks.ToList().AsReadOnly();
        #region  Constructors
        protected Lesson(Guid id, Group group, Teacher teacher, LessonTopic topic, DateTime classTime, LessonStatus status) : base(id)
        {
            LessonValidator.ValidateScheduleTime(classTime);
            Group = group;
            Teacher = teacher;
            ClassTime = classTime;
            Topic = topic;
            State = status;
            if (status == LessonStatus.New)
                teacher.ScheduleLesson(this);
        }
        public Lesson(Group group, Teacher teacher, DateTime classTime, LessonTopic topic) : this(Guid.NewGuid(), group, teacher, topic, classTime, LessonStatus.New)
        {

        }
        protected Lesson() : base(Guid.NewGuid())
        {

        }

        #endregion
        #region Private methods
        /// <summary>
        /// Отмечает урок как проведённый.
        /// </summary>
        public void Teach()
        {
            ValidateBeforeStateChange();
            State = LessonStatus.Teached;
        }

        /// <summary>
        /// Отменяет урок.
        /// </summary>
        public void Cancel()
        {
            ValidateBeforeStateChange();
            State = LessonStatus.Canselled;
        }

        /// <summary>
        /// Переносит урок на другую дату.
        /// </summary>
        public void Reschedule(DateTime time)
        {
            ValidateBeforeStateChange();
            ValidateLessonSchedule(time);
            ClassTime = time;
        }

        /// <summary>
        /// Назначает домашнее задание к уроку.
        /// </summary>
        public void AddHomework(Homework homework)
        {
            if (homework.Lesson != this)
                throw new HomeworkLessonMismatchException(this, homework);

            _homeworks.Add(homework);
        }

        /// <summary>
        /// Получает домашнее задание по теме (если было назначено).
        /// </summary>
        public Homework? GetHomeworkByTopic(LessonTopic topic)
        {
            return _homeworks.FirstOrDefault(h => h.Title.Value == topic.Value);
        }

        #endregion

        #region Validation

        /// <summary>
        /// Проверка, что дата урока не раньше текущей.
        /// </summary>
        private static void ValidateLessonSchedule(DateTime classTime)
        {
            if (classTime.ToUniversalTime() < DateTime.Today.ToUniversalTime())
                throw new InvalidLessonScheduleTimeException(classTime);
        }

        /// <summary>
        /// Проверка перед любым изменением статуса урока.
        /// </summary>
        private void ValidateBeforeStateChange()
        {
            if (State == LessonStatus.Canselled)
                throw new LessonCanselledException(this);
            if (State == LessonStatus.Teached)
                throw new LessonAlreadyTeachedException(this);
        }

        #endregion
    }
}