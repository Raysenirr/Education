using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.EntityFramework.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            // Ключ
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedOnAdd();

            // Value Object: HomeworkTitle
            builder.Property(h => h.Title)
                .HasConversion(
                    title => title.Value,
                    value => new HomeworkTitle(value))
                .HasMaxLength(200)
                .IsRequired();

            // Навигация к уроку
            builder.HasOne(h => h.Lesson)
                .WithMany(l => l.AssignedHomeworks)
                .IsRequired();

            builder.Navigation(h => h.Lesson).AutoInclude();

            // Маппинг приватной коллекции _submissions
            builder.HasMany<HomeworkSubmission>("_submissions")
                .WithOne(s => s.Homework)
                .HasForeignKey(s => s.HomeworkId)
                .IsRequired();

            builder.Navigation("_submissions")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

