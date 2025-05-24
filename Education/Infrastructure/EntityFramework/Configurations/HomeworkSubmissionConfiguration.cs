using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.EntityFramework.Configurations
{
    public class HomeworkSubmissionConfiguration : IEntityTypeConfiguration<HomeworkSubmission>
    {
        public void Configure(EntityTypeBuilder<HomeworkSubmission> builder)
        {
            // Композитный ключ (один студент может сдать одно задание один раз)
            builder.HasKey(x => new { x.StudentId, x.HomeworkId });

            // Настройка даты отправки
            builder.Property(x => x.SubmissionDate)
                   .IsRequired();

            // Навигация к студенту
            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Навигация к домашнему заданию
            builder.HasOne(x => x.Homework)
                   .WithMany(x => x.Submissions)
                   .HasForeignKey(x => x.HomeworkId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}



