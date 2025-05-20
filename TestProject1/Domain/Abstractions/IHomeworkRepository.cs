using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;

namespace Education.Domain.Abstractions
{
    public interface IHomeworkRepository : IRepository<Homework, Guid>
    {
        Task<IEnumerable<Homework>> GetByLessonIdAsync(Guid lessonId);
        Task<IEnumerable<Homework>> GetSubmittedByStudentIdAsync(Guid studentId);
    }
}
