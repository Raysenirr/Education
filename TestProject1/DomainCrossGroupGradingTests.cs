using System;
using Xunit;
using FluentAssertions;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.ValueObjects;
using Education.Domain.Exceptions;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;


public class DomainCrossGroupGradingTests
{
    [Fact]
    public void GradingStudentFromAnotherGroup_ShouldThrow()
    {
        // Arrange
        var groupA = new Group(new GroupName("Group-A-6"));
        var groupB = new Group(new GroupName("Group-B-7"));

        var teacher = new Teacher(new PersonName("Prof Grader"));
        var foreignStudent = new Student(new PersonName("Other Group Student"), groupB); // из другой группы

        // Добавляем шаблон, иначе AssignHomeworkFromBank выбросит исключение
        teacher.HomeworkBank.AddTemplate(
            new LessonTopic("Cross Validation"),
            new HomeworkTitle("Cross Validation HW"));

        var lesson = new Lesson(groupA, teacher, DateTime.UtcNow.AddMinutes(-60), new LessonTopic("Cross Validation"));
        var homework = teacher.AssignHomeworkFromBank(lesson);

        teacher.TeachLesson(lesson);

        // Act
        Action act = () => teacher.GradeStudent(foreignStudent, Mark.Good, lesson, homework);

        // Assert
        act.Should().Throw<LessonNotVisitedException>();
    }
}
