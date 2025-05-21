using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Education.Domain.Entities
{
    /// <summary>
    /// Информация о сданном домашнем задании
    /// </summary>
    public class SubmittedHomeworkInfo
    {
        public Homework Homework { get; private set; }
        public Student Student { get; private set; }
        public DateTime SubmittedAt { get; private set; }

        // Основной публичный конструктор
        public SubmittedHomeworkInfo(Homework homework, Student student, DateTime submittedAt)
        {
            Homework = homework ?? throw new ArgumentNullException(nameof(homework));
            Student = student ?? throw new ArgumentNullException(nameof(student));
            SubmittedAt = submittedAt;
        }

        // Конструктор для EF   (protected)
        protected SubmittedHomeworkInfo()
        {
            // Инициализация для EF
            Homework = null!;
            Student = null!;
        }

        /// <summary>
        /// Проверяет, было ли задание сдано с опозданием
        /// </summary>
        public bool IsLate => SubmittedAt > Homework.Lesson.ClassTime;
    }
}