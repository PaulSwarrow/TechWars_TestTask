using Interfaces;
using UnityEngine;

namespace Actors
{
    public class ProjectileActor : MonoBehaviour, IPoolable
    {
        private Transform _transform;
        public float timestamp;

        public void Reset()
        {
        }

        private void Awake()
        {
            _transform = transform;
        }

        public Vector3 Direction => _transform.forward;

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }
    }
}