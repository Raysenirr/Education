using Education.Domain.Entities.Base;
using Education.Domain.Entities;
using Education.Domain.Enums;
using Education.Domain.Exceptions;
using Education.Domain.ValueObjects.Validators;
using Education.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.ValueObjects.Validators
{
    public static class LessonValidator
    {
        public static void ValidateScheduleTime(DateTime classTime)
        {
            if (classTime.ToUniversalTime() < DateTime.Today.ToUniversalTime())
                throw new InvalidLessonScheduleTimeException(classTime);
        }

        public static void ValidateBeforeTeaching(Lesson lesson)
        {
            if (lesson.State == LessonStatus.Canselled)
                throw new LessonCanselledException(lesson);
            if (lesson.State == LessonStatus.Teached)
                throw new LessonAlreadyTeachedException(lesson);
        }
    }
}
