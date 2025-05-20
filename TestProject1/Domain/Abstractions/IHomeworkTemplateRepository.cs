using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain.Entities;
using Education.Domain.ValueObjects;

namespace Education.Domain.Abstractions
{
    public interface IHomeworkTemplateRepository : IRepository<HomeworkTemplate, Guid>
    {
        Task<HomeworkTemplate?> GetByTopicAsync(LessonTopic topic);
        Task<IEnumerable<HomeworkTemplate>> GetAllByTeacherIdAsync(Guid teacherId);
    }
}