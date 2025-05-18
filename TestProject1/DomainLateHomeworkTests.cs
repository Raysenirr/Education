using System;
using Xunit;
using FluentAssertions;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.ValueObjects;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;


public class DomainLateHomeworkTests
{
    [Fact]
    public void LateSubmission_ShouldBeDetected()
    {
        // Фиксируем текущее время для согласованных проверок
        var now = DateTime.UtcNow;

        // 1. Подготовка
        var group = new Group(new GroupName("Late-1-1"));
        var teacher = new Teacher(new PersonName("ProfDelay"));
        var student = new Student(new PersonName("Scott Summers"), group);

        // 2. Урок был 2 часа назад (чётко в прошлом)
        var lessonDate = now.AddHours(-2);
        var topic = new LessonTopic("Optics");
        var lesson = new Lesson(group, teacher, lessonDate, topic);

        // 3. Учитель проводит урок
        teacher.TeachLesson(lesson);

        // 4. Добавляем шаблон и назначаем домашку
        teacher.HomeworkBank.AddTemplate(topic, new HomeworkTitle("Light HW"));
        var homework = teacher.AssignHomeworkFromBank(lesson);

        // 5. Студент посещает урок
        student.AttendLesson(lesson);

        // 6. Сдача с опозданием (чётко после времени урока)
        var submitDate = lessonDate.AddMinutes(1); // Опоздание на 1 минуту
        student.SubmitHomework(homework, submitDate);

        // 7. Проверяем что сдача действительно поздняя
        homework.IsLate(student).Should().BeTrue();

        // 8. Преподаватель выставляет оценку
        teacher.GradeStudent(student, Mark.Excellent, lesson);

        // 9. Проверяем что оценка понижена
        var grade = student.GetGradeByLesson(lesson);
        grade.Mark.Should().Be(Mark.Satisfactorily);
    }
}