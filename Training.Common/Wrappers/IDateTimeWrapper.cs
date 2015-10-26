using System;

namespace Training.Common.Wrappers
{
    public interface IDateTimeWrapper
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}
