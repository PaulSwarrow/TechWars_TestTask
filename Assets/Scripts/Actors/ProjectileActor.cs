using System;
using DefaultNamespace.Interfaces;
using UnityEngine;

namespace DefaultNamespace.Actors
{
    public class ProjectileActor : MonoBehaviour, IPoolable
    {
        private Transform _transform;

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