using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities.Base;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;
using Education.Domain.Enums;
using System.Diagnostics;
using Education.Domain.Exceptions;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Представляет преподавателя в системе.
    /// </summary>
    public class Teacher(PersonName name) : Person(Guid.NewGuid(), name)
    {
        // Проведённые учителем уроки
        private readonly ICollection<Lesson> _lessons = [];

        // Поставленные оценки
        private readonly ICollection<Grade> _grades = [];

        // Личный банк шаблонов домашних заданий
        private readonly HomeworkBank _homeworkBank = new();
        public HomeworkBank HomeworkBank => _homeworkBank;

        // Уроки, которые были проведены
        public IReadOnlyCollection<Lesson> TeachedLessons =>
            _lessons.Where(l => l.State == LessonStatus.Teached).ToList().AsReadOnly();

        // Уроки, запланированные к проведению
        public IReadOnlyCollection<Lesson> SchedulledLessons =>
            _lessons.Where(l => l.State == LessonStatus.New).ToList().AsReadOnly();

        // Все выставленные оценки
        public IReadOnlyCollection<Grade> AssignedGrades => [.. _grades];

        /// <summary>
        /// Провести урок и отметить его как завершённый.
        /// </summary>
        public void TeachLesson(Lesson lesson)
        {
            if (lesson.State == LessonStatus.Teached)
                throw new LessonAlreadyTeachedException(lesson);

            if (TeachedLessons.Contains(lesson))
                throw new DoubleTeachedLessonException(lesson, this);

            lesson.Teach();

            if (!_lessons.Contains(lesson))
                _lessons.Add(lesson);
        }

        /// <summary>
        /// Выставить оценку студенту за конкретный урок.
        /// </summary>
        public void GradeStudent(Student student, Mark mark, Lesson lesson)
        {
            if (lesson.State != LessonStatus.Teached)
                throw new LessonNotStartedException(lesson);

            if (!_lessons.Contains(lesson))
                throw new AnotherTeacherLessonGradedException(lesson, this);

            if (_grades.Any(g => g.Student == student && g.Lesson == lesson))
                throw new DoubleGradeStudentLesson(lesson, student);

            // Если студент сдал задание с опозданием — оценка максимум 3
            var homework = lesson.GetHomeworkByTopic(lesson.Topic);
            if (homework != null && homework.IsLate(student) && mark > Mark.Satisfactorily)
                mark = Mark.Satisfactorily;

            var grade = new Grade(this, student, lesson, DateTime.Now, mark);
            student.GetGrade(grade);
            _grades.Add(grade);
        }

        /// <summary>
        /// Добавить урок в расписание учителя.
        /// </summary>
        internal void ScheduleLesson(Lesson lesson)
        {
            if (lesson.State != LessonStatus.New)
                throw new LessonAlreadyTeachedException(lesson);

            if (!_lessons.Contains(lesson))
                _lessons.Add(lesson);
        }

        /// <summary>
        /// Назначить домашнее задание из личного банка по теме урока.
        /// </summary>
        public Homework AssignHomeworkFromBank(Lesson lesson)
        {
            var template = _homeworkBank.GetTemplateByTopic(lesson.Topic);

            if (template == null)
                throw new HomeworkTemplateNotFoundException(lesson.Topic, this);

            var homework = new Homework(lesson, template.Title);
            lesson.AddHomework(homework);
            return homework;
        }

        /// <summary>
        /// Получить все группы, в которых ведёт уроки этот учитель.
        /// </summary>
        public IReadOnlyCollection<Group> GetTeachedGroups()
        {
            return _lessons.Select(l => l.Group).Distinct().ToList().AsReadOnly();
        }

        /// <summary>
        /// Получить всех студентов, которых учитель учит (через группы).
        /// </summary>
        public IReadOnlyCollection<Student> GetTeachedStudents()
        {
            return _lessons.SelectMany(l => l.Group.Students).Distinct().ToList().AsReadOnly();
        }

        /// <summary>
        /// Получить все сданные домашние задания для уроков этого учителя.
        /// </summary>
        public IReadOnlyCollection<SubmittedHomeworkInfo> GetSubmittedHomeworks()
        {
            var result = new List<SubmittedHomeworkInfo>();

            foreach (var lesson in TeachedLessons)
            {
                foreach (var homework in lesson.AssignedHomeworks)
                {
                    foreach (var (student, date) in homework.SubmittedBy)
                    {
                        result.Add(new SubmittedHomeworkInfo(homework, student, date));
                    }
                }
            }

            return result.AsReadOnly();
        }
    }
}