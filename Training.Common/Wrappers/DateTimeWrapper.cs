using System;

namespace Training.Common.Wrappers
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
