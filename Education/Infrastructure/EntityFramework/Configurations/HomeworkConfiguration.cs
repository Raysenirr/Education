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

public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
{
    public void Configure(EntityTypeBuilder<Homework> builder)
    {
        // Ключ
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        // ValueObject: Title
        builder.Property(x => x.Title)
            .HasConversion(
                title => title.Value,
                value => new HomeworkTitle(value))
            .IsRequired();

        builder.HasOne(x => x.Lesson)
            .WithMany(l => l.AssignedHomeworks)
            .IsRequired();

    }
}

