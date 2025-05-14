using Education.Domain.Entities.Base;
using Education.Domain.Exceptions;
using Education.Domain.ValueObjects;
using Education.Domain.ValueObjects.Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class Group : Entity<Guid>
    {
        private readonly ICollection<Student> _students = [];
        public IReadOnlyCollection<Student> Students => [.. _students];
        public GroupName Name { get; }
        protected Group(Guid id, GroupName name) : base(id)
        {
            Name = name;

        }
        public Group(GroupName name) : this(Guid.NewGuid(), name)
        {

        }
        protected Group() : base(Guid.NewGuid())
        {
        }
        public void AddStudent(Student student)
        {
            if (_students.Contains(student))
                throw new DoubleEnrollmentException(student, this);
            _students.Add(student);
        }

    }
}
