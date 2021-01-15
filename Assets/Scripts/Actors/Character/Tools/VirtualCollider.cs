using UnityEngine;

namespace Actors.Character.Tools
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

        public bool HitTest(Vector3 position, Vector3 vector, out Vector3 point)
        {
            if (bounds.IntersectRay(
                    new Ray(_transform.InverseTransformPoint(position), _transform.InverseTransformDirection(vector)),
                    out var distance) && distance < vector.magnitude)
            {
                point = position + vector * distance;

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