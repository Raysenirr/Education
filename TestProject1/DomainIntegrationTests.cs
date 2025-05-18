using System;
using Xunit;
using FluentAssertions;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.ValueObjects;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;

public class DomainIntegrationTests
{
    [Fact]
    public void FullScenario_ShouldWork_Correctly()
    {
        // 1. Создание группы, учителя и студента
        var group = new Group(new GroupName("Int-1-1"));
        var teacher = new Teacher(new PersonName("ProfX"));
        var student = new Student(new PersonName("Jean Grey"), group);

        // 2. Создание урока с темой
        var topic = new LessonTopic("Telepathy Basics");
        var lessonDate = DateTime.UtcNow.AddMinutes(-30);
        var lesson = new Lesson(group, teacher, lessonDate, topic);

        // 3. Учитель проводит урок
        teacher.TeachLesson(lesson);

        // 4. Назначается домашнее задание
        teacher.HomeworkBank.AddTemplate(topic, new HomeworkTitle("Read minds HW"));
        var homework = teacher.AssignHomeworkFromBank(lesson);

        // 5. Студент посещает урок и получает задание
        student.AttendLesson(lesson);
        var homeworks = student.GetAssignedHomeworks();
        homeworks.Should().Contain(homework);

        // 6. Студент сдаёт домашку ровно во время урока (вовремя)
        var submitTime = lessonDate;
        student.SubmitHomework(homework, submitTime);

        // 7. Учитель выставляет оценку
        teacher.GradeStudent(student, Mark.Excellent, lesson);

        // 8. Проверка оценки и состояния
        var grade = student.GetGradeByLesson(lesson);
        grade.Should().NotBeNull();
        grade.Mark.Should().Be(Mark.Excellent);
        grade.Teacher.Should().Be(teacher);
        grade.Lesson.Should().Be(lesson);

        // 9. Проверка информации о сдаче
        var submitted = teacher.GetSubmittedHomeworks();

        submitted.Should().ContainSingle(info =>
            info.Homework.Id == homework.Id &&
            info.Student.Id == student.Id &&
            !info.IsLate
        );
    }
}
