using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;

namespace Education.Infrastructure.EntityFramework.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            // Ключ
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Имя преподавателя (Value Object)
            builder.Property(x => x.Name)
                   .HasConversion(
                       name => name.Value,
                       value => new PersonName(value)
                   )
                   .IsRequired()
                   .HasMaxLength(50);

            // Навигации к приватным полям
            builder.Metadata.FindNavigation("_lessons")!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation("_grades")!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            // HomeworkBank — owned type
            builder.OwnsOne(x => x.HomeworkBank, bank =>
            {
                // Навигация к приватной коллекции шаблонов
                bank.Navigation("_templates")
                    .UsePropertyAccessMode(PropertyAccessMode.Field);

                // Конфигурация коллекции шаблонов
                var templatesBuilder = bank.OwnsMany<HomeworkTemplate>("_templates", template =>
                {
                    template.Property(t => t.Topic)
                           .HasConversion(t => t.Value, v => new LessonTopic(v))
                           .HasMaxLength(100)
                           .IsRequired();

                    template.Property(t => t.Title)
                           .HasConversion(t => t.Value, v => new HomeworkTitle(v))
                           .HasMaxLength(100)
                           .IsRequired();

                    template.WithOwner().HasForeignKey("TeacherId");
                    template.HasKey("TeacherId", "Topic");
                });

                templatesBuilder.ToTable("HomeworkTemplates"); // ← сюда перенесено
            });
        }
    }
}
