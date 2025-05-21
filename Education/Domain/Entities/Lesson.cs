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
using System.Collections.ObjectModel;


namespace Education.Domain.Entities
{
    public class Lesson : Entity<Guid>
    {
        public Group Group { get; }
        public Teacher Teacher { get; }
        public DateTime ClassTime { get; private set; }
        public LessonTopic Topic { get; }
        public LessonStatus State { get; private set; }

        private readonly ICollection<Homework> _homeworks = new List<Homework>();
        public IReadOnlyCollection<Homework> AssignedHomeworks =>
            new ReadOnlyCollection<Homework>(_homeworks.ToList());

        protected Lesson(Guid id, Group group, Teacher teacher, LessonTopic topic,
                         DateTime classTime, LessonStatus status) : base(id)
        {
            LessonValidator.ValidateScheduleTime(classTime);
            Group = group ?? throw new GroupIsNullException();
            Teacher = teacher ?? throw new TeacherIsNullException();
            Topic = topic ?? throw new LessonTopicIsNullException();
            ClassTime = classTime;
            State = status;

            if (status == LessonStatus.New)
                teacher.ScheduleLesson(this);
        }

        // конструктор
        public Lesson(Group group, Teacher teacher, DateTime classTime, LessonTopic topic)
            : this(Guid.NewGuid(), group, teacher, topic, classTime, LessonStatus.New) { }

        // EF конструктор
        protected Lesson() : base(Guid.NewGuid()) { }

        public void Teach()
        {
            ValidateBeforeStateChange();
            State = LessonStatus.Teached;
        }

        public void Cancel()
        {
            ValidateBeforeStateChange();
            State = LessonStatus.Canselled;
        }

        public void Reschedule(DateTime time)
        {
            ValidateBeforeStateChange();
            ValidateLessonSchedule(time);
            ClassTime = time;
        }

        public void AddHomework(Homework homework)
        {
            if (homework == null)
                throw new HomeworkIsNullException();

            if (homework.Lesson != this)
                throw new HomeworkLessonMismatchException(this, homework);

            _homeworks.Add(homework);
        }

        public Homework? GetHomeworkById(Guid id)
        {
            return _homeworks.FirstOrDefault(h => h.Id == id);
        }

        public IReadOnlyCollection<HomeworkSubmission> GetAllSubmissions()
        {
            return new ReadOnlyCollection<HomeworkSubmission>(
                _homeworks.SelectMany(h => h.Submissions).ToList());
        }

        private static void ValidateLessonSchedule(DateTime classTime)
        {
            if (classTime.ToUniversalTime() < DateTime.UtcNow.Date)
                throw new InvalidLessonScheduleTimeException(classTime);
        }

        private void ValidateBeforeStateChange()
        {
            if (State == LessonStatus.Canselled)
                throw new LessonCanselledException(this);

            if (State == LessonStatus.Teached)
                throw new LessonAlreadyTeachedException(this);
        }
    }

}