using Education.Domain.Entities.Base;
using Education.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Exceptions;

namespace Education.Domain.Entities
{
    public class Grade : Entity<Guid>
    {
        #region Public readable properties
        public Teacher Teacher { get; }
        public Student Student { get; }
        public Lesson Lesson { get; }
        public DateTime GradedTime { get; }
        public Mark Mark { get; }
        #endregion
        #region Constructors
        protected Grade(Guid id, Teacher teacher, Student student, Lesson lesson, DateTime gradeTime, Mark mark) : base(id)
        {
            if (lesson.State != LessonStatus.Teached)
                throw new LessonNotStartedException(lesson);
            if (lesson.Teacher != teacher)
                throw new AnotherTeacherLessonGradedException(lesson, teacher);
            if (gradeTime.ToUniversalTime() < lesson.ClassTime.ToUniversalTime())
                throw new LessonNotStartedException(lesson);
            if (!student.AttendedLessons.Contains(lesson))
                throw new LessonNotVisitedException(lesson, student);
            Teacher = teacher;
            Student = student;
            Lesson = lesson;
            GradedTime = gradeTime;
            Mark = mark;

        }
        public Grade(Teacher teacher, Student student, Lesson lesson, DateTime gradeTime, Mark mark) : this(Guid.NewGuid(), teacher, student, lesson, gradeTime, mark)
        {

        }
        protected Grade() : base(Guid.NewGuid())
        {

        }
        #endregion
    }
}