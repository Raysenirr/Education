using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.EntityFramework.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        // Ключ
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        // Имя (Value Object)
        builder.Property(x => x.Name)
            .HasConversion(name => name.Value, name => new PersonName(name))
            .IsRequired()
            .HasMaxLength(50);

        // Приватная коллекция уроков, через поле
        builder.Navigation("_lessons")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Приватная коллекция оценок
        builder.Navigation("_grades")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Игнорировать вычисляемые свойства (проекции)
        builder.Ignore(x => x.TeachedLessons);
        builder.Ignore(x => x.SchedulledLessons);
        builder.Ignore(x => x.AssignedGrades);

        // Маппинг HomeworkBank (если он сущность)
        builder.OwnsOne(x => x.HomeworkBank); // если HomeworkBank — Value Object или композиция
    }
}