using System;

namespace Door_of_Soul.Core
{
    public interface IEventDependencyReleasable
    {
        event Action OnEventDependencyDisappear;
        void ReleaseDependency();
    }
}
