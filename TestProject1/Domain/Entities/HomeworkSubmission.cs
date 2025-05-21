using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System;
using Education.Domain.Exceptions;

namespace Education.Domain.Entities
{
    public class HomeworkSubmission
    {
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; }

        public Guid HomeworkId { get; private set; }
        public Homework Homework { get; private set; }

        public DateTime SubmissionDate { get; private set; }

        // Конструктор для EF Core
        private HomeworkSubmission() { }

        public HomeworkSubmission(Student student, Homework homework, DateTime submissionDate)
        {
            Student = student ?? throw new InvalidHomeworkSubmissionException(
                nameof(student));

            Homework = homework ?? throw new InvalidHomeworkSubmissionException(
                nameof(homework));

            if (submissionDate > DateTime.UtcNow.AddMinutes(1))
                throw new InvalidSubmissionDateException(submissionDate);

            if (submissionDate < homework.Lesson.ClassTime.AddMonths(-1))
                throw new InvalidHomeworkSubmissionException(
                    nameof(submissionDate));

            StudentId = student.Id;
            HomeworkId = homework.Id;
            SubmissionDate = submissionDate;
        }
    }
}