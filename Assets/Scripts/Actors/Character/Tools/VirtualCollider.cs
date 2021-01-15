using System;
using UnityEngine;

namespace Tools
{
    public class VirtualCollider : MonoBehaviour
    {
        [SerializeField] private Renderer mesh;
        [SerializeField] private Bounds bounds;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            GameManager.Colliders.RegisterCollider(this);
        }

        private void OnDisable()
        {
            GameManager.Colliders.UnregisterCollider(this);
        }

        public bool HitTest(Vector3 position, Vector3 direction, out Vector3 point)
        {
            if (bounds.IntersectRay(
                new Ray(_transform.InverseTransformPoint(position), _transform.InverseTransformDirection(direction)),
                out var distance))
            {
                point = position + direction * distance;

                return true;
            }

            point = default;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.TransformPoint(bounds.center), transform.TransformVector(bounds.size));
        }
    }
}