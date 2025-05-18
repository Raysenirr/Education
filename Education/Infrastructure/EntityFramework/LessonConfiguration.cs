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

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ClassTime).IsRequired();
        builder.Property(x => x.State).IsRequired();
        builder.Property(x => x.Topic)
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasConversion(topic => topic.Topic, topic => new LessonTopic(topic));
        builder.HasOne(x => x.Group).WithMany();
        builder.HasOne(x => x.Teacher).WithMany("_lessons");
    }
}
