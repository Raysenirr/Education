using System;
using Xunit;
using FluentAssertions;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.ValueObjects;
using Education.Domain.Exceptions;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;


public class DomainHomeworkGroupValidationTests
{
    [Fact]
    public void SubmittingHomeworkFromAnotherGroup_ShouldThrow()
    {
        // Arrange
        var groupA = new Group(new GroupName("Alpha-7-7"));
        var groupB = new Group(new GroupName("Beta-7-7"));

        var teacher = new Teacher(new PersonName("Prof Split"));
        var student = new Student(new PersonName("Wrong Group Student"), groupB); // из другой группы

        var lesson = new Lesson(groupA, teacher, DateTime.UtcNow.AddMinutes(-45), new LessonTopic("Group Theory"));
        teacher.TeachLesson(lesson);

        var homework = new Homework(lesson, new HomeworkTitle("Subgroups"));
        lesson.AddHomework(homework);

        // Act
        Action act = () => student.SubmitHomework(homework, DateTime.UtcNow);

        // Assert
        act.Should().Throw<AnotherGroupLessonException>();
    }
}
