using System;
using Xunit;
using FluentAssertions;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.ValueObjects;
using Education.Domain.Exceptions;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;


public class GradeTests
{
    [Fact]
    public void Constructor_ShouldCreateGrade_WhenValid()
    {
        var group = new Group(new GroupName("G-Grade-1"));
        var teacher = new Teacher(new PersonName("MrGrade"));
        var student = new Student(new PersonName("Sewdfw"), group);
        var topic = new LessonTopic("Grading");
        var lesson = new Lesson(group, teacher, DateTime.UtcNow.AddMinutes(-5), topic);

        teacher.TeachLesson(lesson);
        student.AttendLesson(lesson);

        var now = DateTime.UtcNow;
        var grade = new Grade(teacher, student, lesson, now, Mark.Good);

        grade.Teacher.Should().Be(teacher);
        grade.Student.Should().Be(student);
        grade.Lesson.Should().Be(lesson);
        grade.Mark.Should().Be(Mark.Good);
        grade.GradedTime.Should().BeCloseTo(now, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenLessonNotTeached()
    {
        var group = new Group(new GroupName("G-Grade-1"));
        var teacher = new Teacher(new PersonName("MrGrade"));
        var student = new Student(new PersonName("Sewdfw"), group);
        var topic = new LessonTopic("Grading");
        var lesson = new Lesson(group, teacher, DateTime.UtcNow, topic);

        Action act = () => new Grade(teacher, student, lesson, DateTime.UtcNow, Mark.Satisfactorily);

        act.Should().Throw<LessonNotStartedException>();
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenTeacherMismatch()
    {
        var group = new Group(new GroupName("G-Y-1"));
        var realTeacher = new Teacher(new PersonName("Real"));
        var wrongTeacher = new Teacher(new PersonName("Wrong"));
        var student = new Student(new PersonName("Scxkjzncjn"), group);
        var topic = new LessonTopic("Mismatch");
        var lesson = new Lesson(group, realTeacher, DateTime.UtcNow.AddMinutes(-10), topic);

        realTeacher.TeachLesson(lesson);
        student.AttendLesson(lesson);

        Action act = () => new Grade(wrongTeacher, student, lesson, DateTime.UtcNow, Mark.Good);

        act.Should().Throw<AnotherTeacherLessonGradedException>();
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenStudentDidNotAttend()
    {
        var group = new Group(new GroupName("G-Grade-1"));
        var teacher = new Teacher(new PersonName("MrGrade"));
        var student = new Student(new PersonName("Sewdfw"), group);
        var topic = new LessonTopic("Grading");
        var lesson = new Lesson(group, teacher, DateTime.UtcNow.AddMinutes(-10), topic);

        teacher.TeachLesson(lesson);

        Action act = () => new Grade(teacher, student, lesson, DateTime.UtcNow, Mark.Excellent);

        act.Should().Throw<LessonNotVisitedException>();
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenGradedTimeIsBeforeLesson()
    {
        var group = new Group(new GroupName("G-Grade-1"));
        var teacher = new Teacher(new PersonName("MrGrade"));
        var student = new Student(new PersonName("Sewdfw"), group);
        var topic = new LessonTopic("Grading");
        var lessonTime = DateTime.UtcNow.AddMinutes(10);
        var lesson = new Lesson(group, teacher, lessonTime, topic);

        teacher.TeachLesson(lesson);
        student.AttendLesson(lesson);

        var early = DateTime.UtcNow;

        Action act = () => new Grade(teacher, student, lesson, early, Mark.Poor);

        act.Should().Throw<LessonNotStartedException>();
    }
}
