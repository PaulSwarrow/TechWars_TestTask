using UnityEngine;

namespace Tools
{
    public class VirtualCollider : MonoBehaviour
    {

        [SerializeField] private MeshRenderer mesh;


        private void OnEnable()
        {
            GameManager.Colliders.RegisterCollider(this);
        }

        private void OnDisable()
        {
            GameManager.Colliders.UnregisterCollider(this);
        }

        public bool HitTest(Ray ray, out float distance) => mesh.bounds.IntersectRay(ray, out distance);


    }
}