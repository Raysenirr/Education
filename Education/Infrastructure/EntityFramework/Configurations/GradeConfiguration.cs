﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;
using Education.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Infrastructure.EntityFramework.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Mark).IsRequired();
        builder.Property(x => x.GradedTime).IsRequired().HasConversion
        (
            src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
            dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
        ); ;
        builder.HasOne(x => x.Student).WithMany("_grades");
        builder.HasOne(x => x.Teacher).WithMany("_grades");
        builder.HasOne(x => x.Lesson).WithMany("Grades");
        builder.Navigation(x => x.Student).AutoInclude();
        builder.Navigation(x => x.Teacher).AutoInclude();
        builder.Navigation(x => x.Lesson).AutoInclude();
        builder.HasIndex(x => new { x.Student.Id, x.Lesson }).IsUnique();
    }
}
