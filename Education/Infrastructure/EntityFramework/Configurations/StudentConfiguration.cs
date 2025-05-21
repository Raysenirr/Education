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
                .IsRequired();

            builder.Navigation(x => x.Group).AutoInclude();

            // Приватная коллекция посещённых уроков
            builder.HasMany("_lessons")
                .WithMany() 
        }
    }
}