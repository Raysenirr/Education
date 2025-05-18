using System;
using Xunit;
using FluentAssertions;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.ValueObjects;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;


public class DomainGradingWithoutHomeworkTests
{
    [Fact]
    public void GradeCanBeAssignedWithoutHomework()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var group = new Group(new GroupName("Log-101-2"));
        var teacher = new Teacher(new PersonName("Prof NoHW"));
        var student = new Student(new PersonName("Emma Frost"), group);
        var topic = new LessonTopic("Mental Defense");

        var lesson = new Lesson(group, teacher, now.AddMinutes(-30), topic);
        var homework = new Homework(lesson, new HomeworkTitle("Reflection"));
        // Act
        teacher.TeachLesson(lesson);
        student.AttendLesson(lesson);

        // Преподаватель ставит оценку, несмотря на отсутствие домашки
        teacher.GradeStudent(student, Mark.Good, lesson, homework);

        // Assert
        var grade = student.GetGradeByLesson(lesson);
        grade.Should().NotBeNull();
        grade.Mark.Should().Be(Mark.Good);
        grade.Lesson.Should().Be(lesson);
        grade.Teacher.Should().Be(teacher);
    }
}
