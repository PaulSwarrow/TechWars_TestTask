using System.Collections.Generic;
using Actors.Character.Tools;
using Data;
using UnityEngine;

namespace Systems
{
    public class CollidersSystem : IGameSystem
    {
        private HashSet<VirtualCollider> colliders = new HashSet<VirtualCollider>();

        public void Init()
        {
        }

        public void Start()
        {
        }

        public void Destroy()
        {
        }

        public bool Raycast(Vector3 origin, Vector3 direction, out HitInfo hit)
        {
            foreach (var collider in colliders)
            {
                if (!collider.enabled) continue;
                if (collider.HitTest(origin, direction, out var point))
                {
                    hit = new HitInfo
                    {
                        collider = collider,
                        point = point
                    };
                    return true;
                }
            }

            hit = default;
            return false;
        }

        public void RegisterCollider(VirtualCollider virtualCollider)
        {
            colliders.Add(virtualCollider);
        }

        public void UnregisterCollider(VirtualCollider virtualCollider)
        {
            colliders.Remove(virtualCollider);
        }
    }
}