using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.EntityFramework.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        // Ключ и генерация ID
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        // Маппинг имени (Value Object)
        builder.Property(x => x.Name)
            .HasConversion(name => name.Value, name => new PersonName(name))
            .IsRequired()
            .HasMaxLength(50);

        // Связь с группой
        builder.HasOne(x => x.Group)
            .WithMany(x => x.Students)
            .IsRequired(); // если студент не может быть без группы

        builder.Navigation(x => x.Group).AutoInclude();

        // Приватная коллекция посещённых уроков (если many-to-many нужно)
        builder.HasMany("_lessons")
            .WithMany(); // опционально: имя таблицы

        // Маппинг приватной коллекции оценок
        builder.Navigation("_grades")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Маппинг приватной коллекции сданных ДЗ
        builder.Navigation("_submittedHomeworks")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Игнорируем вычисляемые свойства
        builder.Ignore(x => x.AttendedLessons);
        builder.Ignore(x => x.RecievedGrades); // исправлено название
        builder.Ignore(x => x.SubmittedHomeworks); // если нужно, иначе можно оставить
    }
}
