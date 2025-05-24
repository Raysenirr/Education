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

namespace Education.Infrastructure.EntityFramework.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            // Первичный ключ
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Время проведения урока
            builder.Property(x => x.ClassTime)
                   .IsRequired();

            // Статус урока (enum)
            builder.Property(x => x.State)
                   .IsRequired();

            // Тема урока (value object)
            builder.Property(x => x.Topic)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasConversion(
                       topic => topic.Value,
                       value => new LessonTopic(value)
                   );

            // Навигация к группе
            builder.HasOne(x => x.Group)
                   .WithMany()
                   .IsRequired();

            // Навигация к преподавателю (через private field _lessons)
            builder.HasOne(x => x.Teacher)
                   .WithMany("_lessons")
                   .IsRequired();

            // Навигация к домашним заданиям (private field _homeworks)
            builder.HasMany<Homework>("_homeworks")
                   .WithOne(h => h.Lesson)
                   .HasForeignKey("LessonId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation("_homeworks")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
