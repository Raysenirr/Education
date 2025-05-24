using System;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.EntityFramework.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Ключ и генерация ID
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Маппинг имени (Value Object)
            builder.Property(x => x.Name)
                   .HasConversion(
                       name => name.Value,
                       value => new PersonName(value)
                   )
                   .IsRequired()
                   .HasMaxLength(50);

            // Связь с группой
            builder.HasOne(x => x.Group)
                   .WithMany(x => x.Students)
                   .IsRequired();

            builder.Navigation(x => x.Group).AutoInclude();

            // Приватная коллекция посещённых уроков (many-to-many)
            builder.HasMany<Lesson>("_lessons")
                   .WithMany()
                   .UsingEntity(j => j.ToTable("StudentLessons"));

            builder.Metadata.FindNavigation("_lessons")!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Приватная коллекция оценок (one-to-many)
            builder.HasMany<Grade>("_grades")
                   .WithOne(g => g.Student)
                   .HasForeignKey("StudentId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation("_grades")!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
