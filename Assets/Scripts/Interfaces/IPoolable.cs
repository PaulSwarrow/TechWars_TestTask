using UnityEngine;

namespace DefaultNamespace.Interfaces
{
    public interface IPoolable
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        void Reset();
    }
}