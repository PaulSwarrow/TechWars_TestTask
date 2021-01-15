using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        void Reset();
    }
}