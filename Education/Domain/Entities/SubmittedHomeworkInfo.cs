using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class SubmittedHomeworkInfo
    {
        public Homework Homework { get; }
        public Student Student { get; }
        public DateTime SubmittedAt { get; }

        public SubmittedHomeworkInfo(Homework homework, Student student, DateTime submittedAt)
        {
            Homework = homework;
            Student = student;
            SubmittedAt = submittedAt;
        }

        public bool IsLate => SubmittedAt.Date > Homework.Lesson.ClassTime.Date;
    }
}
